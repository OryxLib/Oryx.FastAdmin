using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.ValueType
{
    public static class DicionaryExtension
    {
        public static bool IsTrue<T, K>(this Dictionary<T, K> source)
        {
            if (source == null || source.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsFalse<T, K>(this Dictionary<T, K> source)
        {
            return !source.IsTrue();
        }

        public static void IsTrueToGo<T, K>(this Dictionary<T, K> source, Action action)
        {
            if (source.IsTrue())
            {
                action();
            }
        }

        public static T IsTrueToGo<T, K, M>(this Dictionary<K, M> source, Func<T> func)
        {
            if (source.IsFalse())
            {
                return func();
            }
            return default(T);
        }

        public static void IsFalseToGo<T, K>(this Dictionary<T, K> source, Action action)
        {
            if (source.IsFalse())
            {
                action();
            }
        }

        public static T IsFalseToGo<T, K, M>(this Dictionary<K, M> source, Func<T> func)
        {
            if (source.IsFalse())
            {
                return func();
            }
            return default(T);
        }
    }
}
