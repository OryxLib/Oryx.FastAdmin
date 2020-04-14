using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Filters;
using Oryx.FastAdmin.Model.Contents;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    [AdminPageRoleAuthentication("admin")]
    [Area("Admin")]
    public class CategoryController : BaseBackendController<Category>
    {
        public CategoryController(SqlSugarClient _dbClient) : base(_dbClient)
        {
        }
    }
}
