using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelType : Attribute
    {
        public ModelType()
        {

        }
        public string Name { get; set; }

        public string ControlName { get; set; } = null;

        public Dictionary<string, string> DataSource { get; set; } = null;

        public bool Required { get; set; }

        public string DataSourceTable { get; set; }

        public string DataSourceTableValue { get; set; }

        public string DataSourceQuery { get; set; }

        public bool ShowOnList { get; set; } = true;

        public int Order { get; set; } = 0;
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ModelBindData : Attribute
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
