using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Model.BusinessModel;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    public class SystemConfigController : BaseBackendController<ConfigEntry>
    {
        public SystemConfigController(SqlSugarClient _dbClient) 
            : base(_dbClient)
        {
        } 
    }
}