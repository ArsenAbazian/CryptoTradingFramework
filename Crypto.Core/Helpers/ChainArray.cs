using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Helpers {
    public class ChainArray<T> : IEnumerable<T>, IList<T>, IResizeableArray, IListSource, IList, IBindingList {
        public ChainArray(int chainCapacity) {
            int bc = 0;
            while(chainCapacity > 0) {
                chainCapacity >>= 1;
                bc++;
            }
            Groups = new List<T[]>(2);
            ChainBitsCount = bc;
        }
        public ChainArray() {
            Groups = new List<T[]>(100);
            ChainBitsCount = 10;
        }

        protected int ChainBitsCount { get; set; }
        protected int ChainSize { get { return 2 << ChainBitsCount; } }

        [XmlIgnore]
        public IThreadManager ThreadManager { get; set; }

        object IResizeableArray.GetItem(int index) {
            return GetItem(index);
        }
        int IResizeableArray.Count {
            get { return Count; }
        }

        protected int GetGroupIndex(int index) { return index >> ChainBitsCount; }
        protected int GetIndex(int index) { return index - ((index >> ChainBitsCount) << ChainBitsCount); }

        event ListChangedEventHandler listChanged;
        public event ListChangedEventHandler ListChanged {
            add {
                for(int i = 0; i < Count; i++)
                    SubscribeEvents(this[i]);
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
        protected List<T[]> Groups { get; private set; }

        public void Add(T item) {
            int gi = GetGroupIndex(Count);
            if(gi >= Groups.Count)
                Groups.Add(new T[ChainSize]);
            this[Count] = item;
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
                UnsubscribeEvents(this[i]);
            Count = 0;
            RaiseListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException("Inset cannot be implemented normally in ChainArray");
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException("RemoveAt cannot be implemented normally in ChainArray");
        }

        public bool Remove(T item) {
            throw new NotImplementedException("Remove cannot be implemented normally in ChainArray");
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new ChainArrayEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new ChainArrayEnumeratorObject<T>(this);
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

        bool IBindingList.AllowRemove => false;

        bool IBindingList.SupportsChangeNotification => true;

        bool IBindingList.SupportsSearching => false;

        bool IBindingList.SupportsSorting => false;

        bool IBindingList.IsSorted => false;

        PropertyDescriptor IBindingList.SortProperty => throw new NotSupportedException();

        ListSortDirection IBindingList.SortDirection => throw new NotSupportedException();

        object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

        T IList<T>.this[int index] {
            get { return GetItem(index); }
            set {
                if(index >= Count) {
                    Add(value);
                    return;
                }
                Groups[GetGroupIndex(index)][GetIndex(index)] = value;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }
        public T this[int index] {
            get { return GetItem(index); }
            set {
                if(index >= Count) {
                    Add(value);
                    return;
                }
                Groups[GetGroupIndex(index)][GetIndex(index)] = value;
                RaiseListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        public T GetItem(int index) {
            if(index >= Count)
                return default(T);
            return Groups[GetGroupIndex(index)][GetIndex(index)];
        }

        public int IndexOf(T item) {
            for(int i = 0; i < Count; i++) {
                if(object.Equals(GetItem(i), item))
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
            return IndexOf(item) != -1;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            for(int i = 0; i < Count; i++)
                array[i + arrayIndex] = GetItem(i);
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

        public T Last() { return Count == 0 ? default(T) : GetItem(Count - 1); }
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
            return IndexOf((T)value) != -1;
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
                array.SetValue(GetItem(i), index + i);
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
    }

    public class ChainArrayEnumerator<T> : IEnumerator<T> {
        public ChainArrayEnumerator(ChainArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected ChainArray<T> Owner { get; private set; }
        T IEnumerator<T>.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.GetItem(CurrentIndex);
            }
        }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.GetItem(CurrentIndex);
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

    public class ChainArrayEnumeratorObject<T> : IEnumerator {
        public ChainArrayEnumeratorObject(ChainArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected ChainArray<T> Owner { get; private set; }

        object IEnumerator.Current {
            get {
                if(CurrentIndex == -1)
                    return default(T);
                if(CurrentIndex >= Owner.Count)
                    return default(T);
                return Owner.GetItem(CurrentIndex);
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
