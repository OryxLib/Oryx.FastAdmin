using Oryx.FastAdmin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Database.DbSchema
{
    public class DataBase : BaseModel
    {
        public string DbName { get; set; }

        public string UserAccountId { get; set; }
    }
}
