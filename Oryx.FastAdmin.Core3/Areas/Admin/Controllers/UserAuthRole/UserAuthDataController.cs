using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Filters;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers.UserAuthRole
{
    [AdminPageRoleAuthentication("admin")]
    [Area("Admin")]
    public class UserAuthDataController : BaseBackendController<UserAccount>
    {
        public UserAuthDataController(SqlSugarClient _dbClient)
            : base(_dbClient)
        {
        }
    }
}