using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.Controller
{
    public class FrontendBaseController : BaseController
    {
        SqlSugarClient dbClient;
        public FrontendBaseController(SqlSugarClient _dbClient)
            : base(_dbClient)
        {
            dbClient = _dbClient;
        }
    }
}
