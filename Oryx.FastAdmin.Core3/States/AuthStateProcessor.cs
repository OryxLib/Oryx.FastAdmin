using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Authentication.Business;
using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.StateManage;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.States
{
    public class AuthStateProcessor
    {
        StateHub stateHub;
        IDistributedCache cache;
        ISessionFeature sessionFeature;
        SqlSugarClient sqlSugarClient;
        public AuthStateProcessor(StateHub _stateHub,
            IDistributedCache _distributedCache,
            ISessionFeature _sessionFeature,
            SqlSugarClient _sqlSugarClient)
        {
            stateHub = _stateHub;
            cache = _distributedCache;
            sessionFeature = _sessionFeature;
            sqlSugarClient = _sqlSugarClient;
            stateHub.RecieveState("userlogin", UserLoginState);
            stateHub.RecieveState("userlogout", UserLogoutState);
        }

        public async Task<bool> IsLogin()
        {
            await sessionFeature.Session.LoadAsync();
            byte[] storeData;

            if (!string.IsNullOrEmpty(UserAuthBusiness.UserAuthFrontendKey) && sessionFeature.Session.TryGetValue(UserAuthBusiness.UserAuthFrontendKey, out storeData))
            {
                var strValue = Encoding.UTF8.GetString(storeData);
                if (!string.IsNullOrEmpty(strValue))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<UserAccount> GetUser()
        {
            await sessionFeature.Session.LoadAsync();
            byte[] storeData;
            var userAccount = new UserAccount();
            if (sessionFeature.Session.TryGetValue(UserAuthBusiness.UserAuthFrontendKey, out storeData))
            {
                var strValue = Encoding.UTF8.GetString(storeData);
                var userId = Guid.Parse(strValue);
                userAccount = await sqlSugarClient.Queryable<UserAccount>().FirstAsync(x => x.Id == userId);
                if (!string.IsNullOrEmpty(strValue))
                {
                    return userAccount;
                }
            }
            return null;
        }

        public void UserLoginState(StateModel stateModel)
        {
            cache.SetString(stateModel.Name, stateModel.Value.ToString());
        }

        public void UserLogoutState(StateModel stateModel)
        {
            cache.Remove(stateModel.Value.ToString());
        }

        public bool UserIsLogin(string userToken)
        {
            return !string.IsNullOrEmpty(cache.GetString(userToken));
        }
    }
}
