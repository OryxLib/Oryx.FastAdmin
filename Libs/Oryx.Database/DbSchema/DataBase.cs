using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Database.DbSchema
{
    public class DataBase
    {
        public Guid Id { get; set; }

        public string DbName { get; set; }

        public string UserAccountId { get; set; }
    }
}
