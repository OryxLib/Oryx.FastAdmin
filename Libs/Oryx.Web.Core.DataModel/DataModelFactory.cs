using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oryx.Web.Core.DataModel
{
    public class DataModelModelFactory
    {
        public static Type[] GetAllTypes()
        {
            var ass = Assembly.GetAssembly(typeof(DataModelModelFactory));
            return ass.GetTypes();
        }
    }
}
