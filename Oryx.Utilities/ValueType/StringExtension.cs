using System;

namespace Oryx.Utilities.ValueType
{
    public static class StringExtension
    {
        public static bool IsTrue(this string source)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            return true;
        }

        public static bool IsFalse(this string source)
        {
            return !source.IsTrue();
        }

        public static void IsFalseToGo(this string source, Action action)
        {
            if (source.IsFalse())
            {
                action();
            }
        }

        public static T IsFalseToGo<T>(this string source, Func<T> func)
        {
            if (source.IsFalse())
            {
                return func();
            }
            return default(T);
        }

        public static int Int(this string source)
        {
            var intresult = 0;
            int.TryParse(source, out intresult);
            return intresult;
        }
    }
}
