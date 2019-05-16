using Crypto.Core.Indicators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public class ResizeableArray<T> : IEnumerable<T>, IList<T> {
        public ResizeableArray() {
            Items = new T[32];
        }

        public int Count { get; private set; }
        public T[] Items { get; private set; }
        
        public void Add(T item) {
            if(Count == Items.Length)
                Resize();
            Items[Count] = item;
            Count++;
        }

        public void Clear() {
            Count = 0;
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
        }
        
        public void RemoveAt(int index) {
            if(index >= Count || index < 0)
                return;
            for(int i = index; i < Count - 1; i++) {
                Items[i] = Items[i + 1];
            }
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

        T IList<T>.this[int index] { get => Items[index]; set => Items[index] = value; }
        public T this[int index] { get => Items[index]; set => Items[index] = value; }

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
            throw new NotImplementedException();
            //int length = Math.Min(array.Length - arrayIndex, Count);
            //for(int i = 0; i < length; i++) {
            //    array[i] = Items[i + arrayIndex];
            //}
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
