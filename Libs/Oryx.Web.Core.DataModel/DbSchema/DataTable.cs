using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Oryx.Database.DbSchema
{
    public class DataTable : BaseModel
    {
        public string Name { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<DataTableItem> DataTableItem { get; set; }

        public bool SoftDelete { get; set; }

        public bool TimeStamp { get; set; }
    }
}
