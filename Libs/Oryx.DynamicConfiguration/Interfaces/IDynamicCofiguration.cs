using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.DynamicConfiguration.Interfaces
{
    public interface IDynamicCofiguration
    {
        void SetConfig<T>(T model) where T : class, new();

        T GetConfig<T>();

        JToken GetConfigJToken<T>();

        JToken this[string idnex]
        {
            get; set;
        }
    }
}
