using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oryx.FastAdmin.Model
{
    public class ModelFactory
    {
        public static Type[] GetAllTypes()
        {
            var ass = Assembly.GetAssembly(typeof(ModelFactory));
            return ass.GetTypes();
        }
    }
}
