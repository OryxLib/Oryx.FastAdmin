using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.BusinessModel;
using Oryx.FastAdmin.Core.Controller;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Installation.Controllers
{
    public class AliyunConfigController : BaseBackendController<AliyunConfigModel>
    {
        public AliyunConfigController(SqlSugarClient _dbClient) : base(_dbClient)
        {
        } 
    }
}