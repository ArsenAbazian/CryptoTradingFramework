using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public class SortedReadOnlyArray<T> : IEnumerable<T>, IList<T>, IResizeableArray, IListSource, IList, IBindingList {
        public SortedReadOnlyArray(CycleArray<T> source) { 
            Source = source;
        }

        public IThreadManager ThreadManager { get; set; }

        object IResizeableArray.GetItem(int index) {
            if(index >= Count)
                return null;
            return Source[GetActualIndex(index)];
        }
        int IResizeableArray.Count {
            get { return Count; }
        }

        public event ListChangedEventHandler ListChanged {
            add { Source.ListChanged += value; }
            remove { Source.ListChanged -= value; }
        }

        event ListChangedEventHandler IBindingList.ListChanged {
            add { ListChanged += value; }
            remove { ListChanged -= value; }
        }

        public int Count { get { return Source.Count; } }
        public CycleArray<T> Source { get; private set; }

        
        public bool AllowNotifyPropertyChanged { get; set; }

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

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new SortedArrayEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new SortedArrayEnumeratorObject<T>(this);
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
            set { }
        }

        protected int GetActualIndex(int index) {
            return Count - 1 - index;
        }

        T IList<T>.this[int index] {
            get { 
                if(index >= Count)
                    return default(T);
                return Source[GetActualIndex(index)]; 
                }
            set { }
        }

        public T this[int index] {
            get { 
                if(index >= Count)
                    return default(T);
                return Source[GetActualIndex(index)]; 
                }
            set {
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
            
        }

        void ICollection<T>.Clear() {
            
        }

        bool ICollection<T>.Contains(T item) {
            return ((IList<T>)this).IndexOf(item) != -1;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            for(int i = 0; i < Count; i++)
                array[i + arrayIndex] = this[i];
        }

        bool ICollection<T>.Remove(T item) {
            throw new NotSupportedException();
        }

        public T Last() { return Count == 0? default(T): Source[0]; }
        
        IList IListSource.GetList() {
            return this;
        }

        int IList.Add(object value) {
            return Count;
        }

        bool IList.Contains(object value) {
            return Source.Contains((T)value);
        }

        void IList.Clear() {
        }

        int IList.IndexOf(object value) {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value) {
            throw new NotSupportedException();
        }

        void IList.Remove(object value) {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index) {
            throw new NotSupportedException();
        }

        void ICollection.CopyTo(Array array, int index) {
            for(int i = 0; i < Count; i++)
                array.SetValue(this[i], index + i);
        }

        object IBindingList.AddNew() {
            throw new NotSupportedException();
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

    public class SortedArrayEnumerator<T> : IEnumerator<T> {
        public SortedArrayEnumerator(SortedReadOnlyArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected SortedReadOnlyArray<T> Owner { get; private set; }
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

    public class SortedArrayEnumeratorObject<T> : IEnumerator {
        public SortedArrayEnumeratorObject(SortedReadOnlyArray<T> owner) {
            Owner = owner;
            CurrentIndex = -1;
        }

        protected int CurrentIndex { get; private set; }
        protected SortedReadOnlyArray<T> Owner { get; private set; }

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
