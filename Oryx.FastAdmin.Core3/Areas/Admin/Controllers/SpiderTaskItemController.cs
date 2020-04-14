using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core.SpiderModel;
using Oryx.FastAdmin.Filters;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    [AdminPageRoleAuthentication("admin")]
    [Area("Admin")]
    public class SpiderTaskItemController : BaseBackendController<SpiderTaskItem>
    {
        public SpiderTaskItemController(SqlSugarClient _dbClient) 
            : base(_dbClient)
        {
        }
    }
}