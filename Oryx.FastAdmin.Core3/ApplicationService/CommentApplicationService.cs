using Oryx.FastAdmin.Core.ApplicationService;
using Oryx.FastAdmin.Model.Contents;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.ApplicationService
{
    public class CommentApplicationService : BaseApplicationService<Comments>
    {
        public CommentApplicationService(SqlSugarClient _dbClient) : base(_dbClient)
        {
        }
    }
}
