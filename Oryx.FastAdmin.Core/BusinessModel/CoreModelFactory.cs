using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oryx.FastAdmin.Core.BusinessModel
{
    public class CoreModelFactory
    {
        public static Type[] GetAllTypes()
        {
            var ass = Assembly.GetAssembly(typeof(CoreModelFactory));
            return ass.GetTypes();
        }
    }
}
