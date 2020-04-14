using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Authentication.Model
{
    public class UserAccount : BaseModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [SugarColumn(IsNullable = true, Length = 255)]
        public Guid? RoleId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public UserRole UserRole { get; set; }
    }
}
