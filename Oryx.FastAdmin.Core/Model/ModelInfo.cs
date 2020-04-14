using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.Model
{
    public class ModelInfo
    {
        public string Name { get; set; }

        public string PropName { get; set; }

        public string TypeName { get; set; }

        public int Order { get; set; }

        public string ControlType { get; set; }

        public bool ShowOnList { get; set; }

        public bool Required { get; set; }

        public Dictionary<string, string> DataSource { get; set; }

        /// <summary>
        /// 当出现dynamicgroup columnname是使用此属性
        /// </summary>
        public List<ModelInfo> SubModelInfoList { get; set; }
    }
}
