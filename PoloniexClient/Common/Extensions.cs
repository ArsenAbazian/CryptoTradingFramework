using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoMarketClient.Common {
    public static class DateTimeExtensions {
        public static int ToUnixTime(this DateTime dt) {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)(dt - unixEpoch).TotalSeconds;
        }
    }

    public static class DictionaryExtensions {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue newValue) {
            return GetOrAdd(dictionary, key, newValue, false);
        }

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue newValue, bool threadSafe) {
            if (threadSafe) {
                lock (dictionary)
                    return GetOrAddCore(dictionary, key, newValue);
            } else {
                return GetOrAddCore(dictionary, key, newValue);
            }
        }

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValueDelegate) {
            return GetOrAdd(dictionary, key, newValueDelegate, false);
        }

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValueDelegate, bool threadSafe) {
            if (threadSafe) {
                lock (dictionary)
                    return GetOrAddCore(dictionary, key, newValueDelegate);
            } else {
                return GetOrAddCore(dictionary, key, newValueDelegate);
            }
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) {
            return Get(dictionary, key, false);
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, bool threadSafe) {
            if (dictionary == null || key == null)
                return default(TValue);
            if (threadSafe) {
                lock (dictionary)
                    return GetCore(dictionary, key);
            } else {
                return GetCore(dictionary, key);
            }
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue fallbackValue) {
            return Get(dictionary, key, fallbackValue, false);
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue fallbackValue, bool threadSafe) {
            if (dictionary == null || key == null)
                return fallbackValue;
            if (threadSafe) {
                lock (dictionary)
                    return GetFallbackCore(dictionary, key, fallbackValue);
            } else {
                return GetFallbackCore(dictionary, key, fallbackValue);
            }
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getFallbackValueDelegate) {
            return Get(dictionary, key, getFallbackValueDelegate, false);
        }

        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getFallbackValueDelegate, bool threadSafe) {
            if (dictionary == null || key == null)
                return getFallbackValueDelegate();
            if (threadSafe) {
                lock (dictionary)
                    return GetFallbackCore(dictionary, key, getFallbackValueDelegate);
            } else {
                return GetFallbackCore(dictionary, key, getFallbackValueDelegate);
            }
        }

        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            Set(dictionary, key, value, false);
        }

        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, bool threadSafe) {
            if (dictionary == null)
                return;

            if (threadSafe) {
                lock (dictionary)
                    SetCore(dictionary, key, value);
            } else {
                SetCore(dictionary, key, value);
            }
        }

        static TValue GetOrAddCore<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue notFoundValue) {
            TValue value;
            if (!dictionary.TryGetValue(key, out value)) {
                dictionary[key] = notFoundValue;
                value = notFoundValue;
            }
            return value;
        }

        static TValue GetOrAddCore<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> notFoundValueDelegate) {
            TValue value;
            if (!dictionary.TryGetValue(key, out value)) {
                TValue notFoundValue = notFoundValueDelegate();
                dictionary[key] = notFoundValue;
                value = notFoundValue;
            }
            return value;
        }

        static TValue GetCore<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
                value = default(TValue);
            return value;
        }

        static TValue GetFallbackCore<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue fallbackValue) {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
                value = fallbackValue;
            return value;
        }

        static TValue GetFallbackCore<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getFallbackValueDelegate) {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
                value = getFallbackValueDelegate();
            return value;
        }

        static void SetCore<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            dictionary[key] = value;
        }
    }
}
