using Oryx.FastAdmin.Core.ApplicationService;
using Oryx.FastAdmin.Model.Contents;
using Oryx.FastAdmin.Model.UserInfo;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.ApplicationService
{
    public class UserApplicationService : BaseApplicationService<UserInfoEntry>
    {
        public UserApplicationService(SqlSugarClient _dbClient) 
            : base(_dbClient)
        {
        }

        public UserInfoEntry GetUserInfo(string UserId)
        {
            return Query(x => x.ArthorId == UserId)?.First();
        }
        public List<UserInfoEntry> GetUserInfo(List<string> UserId)
        {
            return Query(x => UserId.Contains(x.ArthorId));
        }
    }
}
