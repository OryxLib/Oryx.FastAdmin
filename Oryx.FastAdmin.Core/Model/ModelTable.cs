using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Oryx.FastAdmin.Core.Model
{
    public class ModelTable
    {
        public List<TableInfo> TableInfoList { get; set; }
        //public List<Dictionary<string, string>> DataList { get; set; }
    }

    public class TableInfo
    {
        public string PropName { get; set; }

        public string Name { get; set; }
    }
}
