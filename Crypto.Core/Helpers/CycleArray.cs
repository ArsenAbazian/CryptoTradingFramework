using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Crypto.Core.Helpers {
    public interface IThreadLock {
        bool ThreadLock { get; set; }
    }
    public class CycleArray<T> : IEnumerable<T>, IList<T>, IResizeableArray, IListSource, IList, IBindingList, IThreadLock {
        public CycleArray(int capacity) { 
            Items = new T[capacity];
        }

        public bool ThreadLock { get; set; }
        public IThreadManager ThreadManager { get; set; }

        object IResizeableArray.GetItem(int index) {
            if(index >= Count)
                return null;
            return Items[index];
        }
        int IResizeableArray.Count {
            get { return Count; }
        }

        event ListChangedEventHandler listChanged;
        public event ListChangedEventHandler ListChanged {
            add {
                for(int i = 0; i < Count; i++)
                    SubscribeEvents(Items[i]);
                AllowNotifyPropertyChanged = true;
                listChanged += value;
            }
            remove {
                listChanged -= value;
            }
        }

        event ListChangedEventHandler IBindingList.ListChanged {
            add { ListChanged += value; }
            remove { ListChanged -= value; }
        }

        public static CycleArray<T> FromList(List<T> list) {
            CycleArray<T> res = new CycleArray<T>(list.Count);
            foreach(T item in list) {
                res.Add(item);
            }
            return res;
        }

        protected void RaiseListChanged(ListChangedEventArgs e) {
            if(listChanged != null) {
                if(ThreadManager != null && ThreadManager.IsMultiThread) {
                    ThreadManager.Invoke((sender, ee) => listChanged(sender, ee), this, e);
                }
                else 
                    listChanged(this, e);
            }
        }

        protected int End { get; private set; } = -1;
        protected int Start { get; private set; } = 0;
        
        public int Count { get; private set; }
        public T[] Items { get; private set; }

        public void AddFirst(T item) {
            Start--;
            if(Start < 0)
                Start = Items.Length - 1;
            if(Count == Items.Length && Start == End) 
                End--;
            if(End < 0)
                End = Items.Length - 1;
            Count = Math.Min(Count + 1, Items.Length);
            Items[Start] = item;
            OnInsert(item, Start);
        }

        public void Add(T item) {
            End++;
            if(End >= Items.Length)
                End = 0;
            if(Count == Items.Length && End == Start)
                Start++;
            if(Start >= Items.Length)
                Start = 0;
            Count = Math.Min(Count + 1, Items.Length);
            Items[End] = item;
            OnInsert(item, End);
        }
        
        public bool AllowNotifyPropertyChanged { get; set; }
        private void SubscribeEvents(object item) {
            if(!AllowNotifyPropertyChanged)
                return;
            if(item is INotifyPropertyChanged) {
                ((INotifyPropertyChanged)item).PropertyChanged -= OnItemPropertyChanged;
                ((INotifyPropertyChanged)item).PropertyChanged += OnItemPropertyChanged;
            }
        }

        private void UnsubscribeEvents(object item) {
            if(!AllowNotifyPropertyChanged)
                return;
            if(item is INotifyPropertyChanged)
                ((INotifyPropertyChanged)item).PropertyChanged -= OnItemPropertyChanged;
        }

        private void OnInsert(T item, int index) {
            //RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, index));
            if(this.updateCount == 0)
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            //RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            SubscribeEvents(item);   
        }

        protected virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
            int index = GetItemIndex((T)sender, SearchFromEnd);
            if(this.updateCount == 0)
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        public bool SearchFromEnd { get; set; } = true;
        protected int GetItemIndex(T item, bool searchFromEnd) {
            if(searchFromEnd) {
                for(int i = Count - 1; i >= 0; i--) {
                    if(object.Equals(this[i], item))
                        return i;
                }
            }
            else {
                for(int i = 0; i < Count; i++) {
                    if(object.Equals(this[i], item))
                        return i;
                }
            }
            return -1;
        }

        public void Clear() {
            for(int i = 0; i < Count; i++)
                UnsubscribeEvents(Items[i]);
            Count = 0;
            End = -1;
            if(this.updateCount > 0)
                return;
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new CycleArrayEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new CycleArrayEnumeratorObject<T>(this);
        }

        int ICollection<T>.Count => Count;

        bool ICollection<T>.IsReadOnly => false;

        bool IListSource.ContainsListCollection => false;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        int ICollection.Count => Count;

        object ICollection.SyncRoot => this;

        bool ICollection.IsSynchronized => true;

        bool IBindingList.AllowNew => true;

        bool IBindingList.AllowEdit => true;

        bool IBindingList.AllowRemove => true;

        bool IBindingList.SupportsChangeNotification => true;

        bool IBindingList.SupportsSearching => false;

        bool IBindingList.SupportsSorting => false;

        bool IBindingList.IsSorted => false;

        PropertyDescriptor IBindingList.SortProperty => throw new NotSupportedException();

        ListSortDirection IBindingList.SortDirection => throw new NotSupportedException();

        object IList.this[int index] { 
            get { return this[index]; } 
            set {  this[index] = (T)value; }
        }

        protected int GetActualIndex(int index) {
            int resIndex = Start + index;
            if(resIndex >= Items.Length)
                resIndex -= Items.Length;
            return resIndex;
        }

        T IList<T>.this[int index] {
            get { 
                if(index >= Count)
                    return default(T);
                return Items[GetActualIndex(index)]; 
                }
            set {
                if(index >= Count)
                    return;
                Items[GetActualIndex(index)] = value;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        public T this[int index] {
            get { 
                if(index >= Count)
                    return default(T);
                return Items[GetActualIndex(index)]; 
                }
            set {
                if(index >= Count)
                    return;
                Items[GetActualIndex(index)] = value;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        public int IndexOf(T item) {
            for(int i = 0; i < Count; i++) {
                if(object.Equals(this[i], item))
                    return i;
            }
            return -1;
        }
        int IList<T>.IndexOf(T item) {
            return IndexOf(item);
        }

        void IList<T>.Insert(int index, T item) {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index) {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item) {
            Add(item);
        }

        void ICollection<T>.Clear() {
            Clear();
        }

        bool ICollection<T>.Contains(T item) {
            return ((IList<T>)this).IndexOf(item) != -1;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            for(int i = 0; i < Count; i++)
                array[i + arrayIndex] = this[i];
        }

        bool ICollection<T>.Remove(T item) {
            throw new NotImplementedException();
        }

        public T Last() { return Count == 0? default(T): Items[Count - 1]; }
        public void AddRange(ResizeableArray<T> data) {
            for(int i = 0; i < data.Count; i++) {
                Add(data[i]);
            }
        }
        public void AddRangeReversed(ResizeableArray<T> data) {
            for(int i = data.Count - 1; i >= 0; i--) {
                Add(data[i]);
            }
        }

        IList IListSource.GetList() {
            return this;
        }

        int IList.Add(object value) {
            Add((T)value);
            return Count;
        }

        bool IList.Contains(object value) {
            return Items.Contains((T)value);
        }

        void IList.Clear() {
            Clear();
        }

        int IList.IndexOf(object value) {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value) {
            throw new NotImplementedException();
        }

        void IList.Remove(object value) {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index) {
            throw new NotImplementedException();
        }

        void ICollection.CopyTo(Array array, int index) {
            for(int i = 0; i < Count; i++)
                array.SetValue(this[i], index + i);
        }

        object IBindingList.AddNew() {
            return typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
        }

        void IBindingList.AddIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor property, object key) {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property) {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort() {
            throw new NotSupportedException();
        }

        int updateCount = 0;
        public void BeginUpdate() {
            this.updateCount++;
        }

        public void EndUpdate() {
            if(this.updateCount == 0)
                return;
            this.updateCount--;
            if(this.updateCount == 0)
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    public class CycleArrayEnumerator<T> : IEnumerator<T> {
        public CycleArrayEnumerator(CycleArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected CycleArray<T> Owner { get; private set; }
        T IEnumerator<T>.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner[CurrentIndex];
            }
        }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner[CurrentIndex];
            }
        }

        void IDisposable.Dispose() {
            
        }

        bool IEnumerator.MoveNext() {
            CurrentIndex++;
            if(CurrentIndex >= Owner.Count)
                return false;
            return true;
        }

        void IEnumerator.Reset() {
            CurrentIndex = -1;
        }
    }

    public class CycleArrayEnumeratorObject<T> : IEnumerator {
        public CycleArrayEnumeratorObject(CycleArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected CycleArray<T> Owner { get; private set; }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner[CurrentIndex];
            }
        }

        bool IEnumerator.MoveNext() {
            CurrentIndex++;
            if(CurrentIndex >= Owner.Count)
                return false;
            return true;
        }

        void IEnumerator.Reset() {
            CurrentIndex = -1;
        }
    }
}
