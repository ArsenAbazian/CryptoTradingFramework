using Crypto.Core.Indicators;
using Crypto.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Helpers {
    public interface IResizeableArray {
        object GetItem(int index);
        int Count { get; }
    }

    public interface IThreadManager {
        bool IsMultiThread { get; }
        void Invoke(Action<object, ListChangedEventArgs> a, object sender, ListChangedEventArgs e);
    }

    public class ResizeableArray<T> : IEnumerable<T>, IList<T>, IResizeableArray, IListSource, IList, IBindingList {
        public ResizeableArray() {
            Items = new T[32];
        }

        public ResizeableArray(int count) {
            Items = new T[count];
        }

        [XmlIgnore]
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
            add {
                ListChanged += value;
            }

            remove {
                ListChanged -= value;
            }
        }

        public static ResizeableArray<T> FromList(List<T> list) {
            ResizeableArray<T> res = new ResizeableArray<T>(list.Count);
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

        public int Count { get; private set; }
        public T[] Items { get; private set; }
        
        public void Add(T item) {
            if(Count == Items.Length)
                Resize();
            Items[Count] = item;
            Count++;
            OnInsert(item, Count - 1);
            
        }

        public bool AllowNotifyPropertyChanged { get; set; }
        public bool SearchFromEnd { get; set; } = true;

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
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            SubscribeEvents(item);   
        }

        protected virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
            int index = GetItemIndex((T)sender, SearchFromEnd);
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        protected int GetItemIndex(T item, bool searchFromEnd) {
            if(searchFromEnd) {
                for(int i = Count - 1; i >= 0; i--) {
                    if(object.Equals(Items[i], item))
                        return i;
                }
            }
            else {
                for(int i = 0; i < Count; i++) {
                    if(object.Equals(Items[i], item))
                        return i;
                }
            }
            return -1;
        }

        public void Clear() {
            for(int i = 0; i < Count; i++)
                UnsubscribeEvents(Items[i]);
            Count = 0;
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public void Insert(int index, T item) {
            if(index >= Count) {
                Add(item);
                return;
            }
            if(index < 0)
                throw new ArgumentOutOfRangeException();
            if(Count == Items.Length)
                Resize();
            for(int i = index; i < Count; i++) {
                Items[i + 1] = Items[i];
            }
            Items[index] = item;
            Count++;
            OnInsert(item, index);
        }
        
        public void RemoveAt(int index) {
            if(index >= Count || index < 0)
                return;
            for(int i = index; i < Count - 1; i++) {
                Items[i] = Items[i + 1];
            }
            //RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, index));
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            Count--;
        }

        public bool Remove(T item) {
            int index = ((IList<T>)this).IndexOf(item);
            if(index < 0)
                return false;
            if(index >= Count)
                return false;
            RemoveAt(index);
            return true;
        }

        private void Resize() {
            T[] newItems = new T[Items.Length * 2];
            for(int i = 0; i < Items.Length; i++)
                newItems[i] = Items[i];
            Items = newItems;
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new StackEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new StackEnumeratorObject<T>(this);
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

        object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

        T IList<T>.this[int index] {
            get { return Items[index]; }
            set {
                while(index >= Items.Length)
                    Resize();
                Items[index] = value;
                if(index >= Count)
                    Count = index + 1;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }
        public T this[int index] {
            get { return Items[index]; }
            set {
                while(index >= Items.Length)
                    Resize();
                Items[index] = value;
                if(index >= Count)
                    Count = index + 1;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        public int IndexOf(T item) {
            for(int i = 0; i < Count; i++) {
                if(object.Equals(Items[i], item))
                    return i;
            }
            return -1;
        }

        int IList<T>.IndexOf(T item) {
            return IndexOf(item);
        }

        void IList<T>.Insert(int index, T item) {
            Insert(index, item);
        }

        void IList<T>.RemoveAt(int index) {
            RemoveAt(index);
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
                array[i + arrayIndex] = Items[i];
        }

        bool ICollection<T>.Remove(T item) {
            return Remove(item);
        }
        public void Push(T item) {
            Add(item);
        }
        public void Pop() {
            if(Count > 0)
                Count--;
        }

        public T Last() { return Count == 0? default(T): Items[Count - 1]; }
        public void AddRange(ResizeableArray<T> data) {
            for(int i = 0; i < data.Count; i++) {
                Add(data.Items[i]);
            }
        }
        public void AddRangeReversed(ResizeableArray<T> data) {
            for(int i = data.Count - 1; i >= 0; i--) {
                Add(data.Items[i]);
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
            Insert(index, (T)value);
        }

        void IList.Remove(object value) {
            Remove((T)value);
        }

        void IList.RemoveAt(int index) {
            RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int index) {
            for(int i = 0; i < Count; i++)
                array.SetValue(Items[i], index + i);
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

        public void Insert(int startIndex, ResizeableArray<T> data) {
            T[] prevItems = Items;
            int prevCount = Count;

            int maxCount = data.Count + Count > Items.Length ? data.Count + Count : Items.Length;
            Items = new T[maxCount];
            
            for(int i = 0; i < startIndex; i++)
                Items[i] = prevItems[i];

            int count = data.Count;
            for(int i = 0; i < count; i++) {
                Items[startIndex + i] = data[i];
            }

            startIndex += count;
            for(int i = prevCount - 1; i >= startIndex; i--)
                Items[startIndex + i] = prevItems[i];
            
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public override string ToString() {
            return string.Format("Array [{0}]", Count);
        }
    }

    public class StackEnumerator<T> : IEnumerator<T> {
        public StackEnumerator(ResizeableArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected ResizeableArray<T> Owner { get; private set; }
        T IEnumerator<T>.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.Items[CurrentIndex];
            }
        }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.Items[CurrentIndex];
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

    public class StackEnumeratorObject<T> : IEnumerator {
        public StackEnumeratorObject(ResizeableArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected ResizeableArray<T> Owner { get; private set; }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.Items[CurrentIndex];
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
