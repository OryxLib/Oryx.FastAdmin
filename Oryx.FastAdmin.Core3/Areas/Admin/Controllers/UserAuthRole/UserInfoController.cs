using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core.Model;
using Oryx.FastAdmin.Core.ViewModel;
using Oryx.FastAdmin.Filters;
using Oryx.FastAdmin.Model;
using Oryx.FastAdmin.Model.UserInfo;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    [AdminPageRoleAuthentication("admin")]
    [Area("Admin")]
    public class UserInfoController : BaseBackendController<UserInfoEntry>
    {
        public UserInfoController(SqlSugarClient _dbClient)
            : base(_dbClient)
        {
        }
    }
}