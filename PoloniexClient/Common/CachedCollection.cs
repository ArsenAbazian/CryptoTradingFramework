using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CryptoMarketClient.Common {
    public abstract class CachedList<TKey, TValue> : BindingList<TValue> {
        Dictionary<TKey, TValue> cache;

        public CachedList() {
        }

        public CachedList(IList<TValue> list)
            : base(list) {
            int count = list.Count;
            for (int i = 0; i < count; i++) {
                TValue item = list[i];
                Cache[GetKey(item)] = item;
            }
        }

        public TValue this[TKey key] {
            get { return Cache.Get(key); }
        }

        protected Dictionary<TKey, TValue> Cache {
            get { return this.cache ?? (this.cache = new Dictionary<TKey, TValue>()); }
        }

        protected override void InsertItem(int index, TValue item) {
            base.InsertItem(index, item);
            Cache[GetKey(item)] = item;
        }

        protected override void RemoveItem(int index) {
            TValue item = Items[index];
            base.RemoveItem(index);
            Cache.Remove(GetKey(item));
        }

        protected override void SetItem(int index, TValue item) {
            TValue oldItem = Items[index];
            base.SetItem(index, oldItem);
            Cache.Remove(GetKey(oldItem));
            Cache[GetKey(item)] = item;
        }

        protected override void ClearItems() {
            base.ClearItems();
            Cache.Clear();
        }

        protected override void OnListChanged(ListChangedEventArgs e) {
            base.OnListChanged(e);
            if (e.ListChangedType == ListChangedType.ItemChanged) {
                TValue item = Items[e.NewIndex];
                Cache[GetKey(item)] = item;
            }
        }

        protected abstract TKey GetKey(TValue item);
    }
}
