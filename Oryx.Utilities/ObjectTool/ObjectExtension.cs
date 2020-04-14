using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Dynamic;

namespace Oryx.Utilities.ObjectTool
{
    public static class ObjectExtension
    {
        public static T IsNotNull<T>(this object source, Func<T> func)
            where T : class
        {
            if (source != null)
            {
                return func();
            }
            return null;
        }

        public static string GetValueToString<T>(this T source, string key)
        {
            var tType = typeof(T);
            var prop = tType.GetProperties().FirstOrDefault(x => x.Name == key);
            return prop?.GetValue(key)?.ToString();
        }

        //public static string GetValueToString<T>(this object source, string key)
        //{
        //    var tType = typeof(T);
        //    var prop = tType.GetProperties().FirstOrDefault(x => x.Name == key);
        //    return prop?.GetValue(key)?.ToString();
        //}
    }
}
