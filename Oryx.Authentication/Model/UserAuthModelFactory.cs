using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oryx.Authentication.Model
{
    public class UserAuthModelFactory
    {
        public static Type[] GetAllTypes()
        {
            var ass = Assembly.GetAssembly(typeof(UserAuthModelFactory));
            return ass.GetTypes();
        }
    }
}
