using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Database.DbSchema
{
    public class DataTableItem : BaseModel
    {
        public Guid TableId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public bool Primary { get; set; }

        public string DefaultValue { get; set; }

        public bool AutoIncreament { get; set; }

        public bool Nullable { get; set; }

        public bool Unique { get; set; }

        public bool Index { get; set; }

        public bool Unsigned { get; set; }

        [SugarColumn(IsIgnore = true)]
        public KeyValuePair<string, string> ForeignKey { get; set; }
    }
}
