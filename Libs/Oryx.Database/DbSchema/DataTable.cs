using System;
using System.Collections.Generic;

namespace Oryx.Database.DbSchema
{
    public class DataTable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<DataTableItem> DataTableItem { get; set; }

        public bool SoftDelete { get; set; }

        public bool TimeStamp { get; set; }
    }
}
