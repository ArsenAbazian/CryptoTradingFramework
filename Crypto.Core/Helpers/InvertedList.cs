using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Crypto.Core.Helpers;

namespace Crypto.Net.Core.Helpers;

public class InvertedList<T> : IReadOnlyList<T>, IResizeableArray, IListSource, IList, IBindingList, ICollection<T>
{
    public InvertedList(IList<T> source)
    {
        _source = source;
    }

    private readonly IList<T> _source;
    
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object GetItem(int index)
    {
        return _source[_source.Count - 1 - index];
    }

    public void CopyTo(Array array, int index)
    {
        for(int i = 0; i < Count; i++)
            array.SetValue(this[i], index + i);
    }

    bool ICollection<T>.Remove(T item)
    {
        throw new NotImplementedException();
    }

    public int Count => _source.Count;
    int ICollection.Count => _source.Count;

    int IResizeableArray.Count => _source.Count;

    int IReadOnlyCollection<T>.Count => _source.Count;

    public int Add(object value)
    {
        _source.Insert(0, (T)value);
        return 0;
    }

    void ICollection<T>.Add(T item)
    {
        Add(item);
    }

    public void Clear()
    {
        _source.Clear();
    }

    bool ICollection<T>.Contains(T item)
    {
        return Contains(item);
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        CopyTo(array, arrayIndex);
    }

    public bool Contains(object value)
    {
        return _source.Contains((T)value);
    }

    public int IndexOf(object value)
    {
        int index = IndexOf(value);
        if(index == -1)
            return -1;
        return _source.Count - index - 1;
    }

    public void Insert(int index, object value)
    {
        _source.Insert(_source.Count - index, (T)value);
    }

    public void Remove(object value)
    {
        _source.Remove((T)value);
    }

    public void RemoveAt(int index)
    {
        _source.RemoveAt(_source.Count - index - 1);
    }

    object IList.this[int index]
    {
        get => _source[_source.Count - 1 - index];
        set => _source[_source.Count - 1 - index] = (T)value;
    }

    public T this[int index] => _source[_source.Count - 1 - index];

    public IList GetList()
    {
        return this;
    }

    bool IListSource.ContainsListCollection => false;
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

    int ICollection<T>.Count => _source.Count;

    bool ICollection<T>.IsReadOnly => false;

    bool IList.IsReadOnly => false;

    bool IList.IsFixedSize => false;

    object ICollection.SyncRoot => this;

    bool ICollection.IsSynchronized => true;

    bool IBindingList.AllowNew => true;

    bool IBindingList.AllowEdit => true;

    bool IBindingList.AllowRemove => true;

    bool IBindingList.SupportsChangeNotification => true;

    bool IBindingList.SupportsSearching => false;

    bool IBindingList.SupportsSorting => false;

    public event ListChangedEventHandler ListChanged;

    bool IBindingList.IsSorted => false;

    PropertyDescriptor IBindingList.SortProperty => throw new NotSupportedException();

    ListSortDirection IBindingList.SortDirection => throw new NotSupportedException();
}