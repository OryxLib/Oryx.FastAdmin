using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.ValueType
{
    public static class BooleanExtension
    {
        public static void TrueAction(this bool srcValue, Action action)
        {
            if (srcValue)
            {
                action();
            }
        }

        public static void FalseAction(this bool srcValue, Action action)
        {
            if (!srcValue)
            {
                action();
            }
        }

        public static T TrueFunction<T>(this bool srcValue, Func<T> action)
        {
            if (srcValue)
            {
                return action();
            }
            else
            {
                return default(T);
            }
        }

        public static T FalseFunction<T>(this bool srcValue, Func<T> action)
        {
            if (!srcValue)
            {
                return action();
            }
            else
            {
                return default(T);
            }
        }
    }
}
