using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.StateManage;
using Oryx.Utilities.Security;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Oryx.Wx.WebApp;
using Microsoft.Extensions.Configuration;
using Oryx.FastAdmin.Model.UserInfo;

namespace Oryx.Authentication.Business
{
    public class UserAuthBusiness
    {
        SqlSugarClient sugarClient;
        StateHub stateHub;

        public static string UserAccountSessionkey { get; set; }

        public const string UserAuthFrontendKey = "Frontend";

        public const string UserAuthBackendKey = "Backend";

        private IConfiguration configuration;

        public UserAuthBusiness(SqlSugarClient _sugarClient, StateHub _stateHub, IConfiguration _configuration)
        {
            sugarClient = _sugarClient;
            stateHub = _stateHub;
            configuration = _configuration;
        }

        public async Task RegisterUser(UserAccount userAccount)
        {
            var userModel = new UserAccount
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Password = MD5Tool.GetMD532(userAccount.Password),
                UserName = userAccount.UserName
            };
            await sugarClient.Insertable<UserAccount>(userModel).ExecuteCommandAsync();

            var userCount = await sugarClient.Queryable<UserAccount>().CountAsync();

            await sugarClient.Insertable<UserInfoEntry>(new UserInfoEntry
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                ArthorId = userModel.Id.ToString(),
                Avarta = "~/images/avarta.png",
                NicName = "用户" + userCount.ToString().PadLeft(4, '0')
            }).ExecuteCommandAsync();
        }

        public async Task<UserAccount> Login(UserAccount userAccount)
        {
            try
            {
                var userInfo = await sugarClient.Queryable<UserAccount>().FirstAsync(x => x.UserName == userAccount.UserName && x.Password == MD5Tool.GetMD532(userAccount.Password));
                if (userInfo != null)
                {
                    stateHub.SendState(new StateModel
                    {
                        Name = "userlogin",
                        Value = userInfo.Id
                    });
                }

                return userInfo;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public async Task<UserAccount> GetUser(Guid Id)
        {
            try
            {
                var userInfo = await sugarClient.Queryable<UserAccount>().FirstAsync(x => x.Id == Id);
                return userInfo;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public async Task LogOut(string userToken)
        {
            stateHub.SendState(new StateModel
            {
                Value = userToken
            });
        }

        public async Task<List<string>> GetRoles(string value)
        {
            return new List<string> {
                "admin","user"
            };
        }

        /// <summary>
        /// 微信网页登录
        /// 兼容服务端渲染与前端渲染页面
        /// </summary>
        /// <returns>用户Id</returns>
        public async Task<string> WxWebLoin(WebAppAuthUserInfo userInfo)
        {
            var targetUserAccount = await sugarClient.Queryable<UserWxAccountMapping>()
                .Where(x => x.UserId == userInfo.UnionId && x.WxOpenId == userInfo.OpenId).FirstAsync();
            if (targetUserAccount == null)
            {
                targetUserAccount = new UserWxAccountMapping
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid().ToString(),
                    AppId = configuration["wx:appId"],
                    WxOpenId = userInfo.OpenId,
                    CreateTime = DateTime.Now
                };
                await sugarClient.Insertable<UserWxAccountMapping>(targetUserAccount).ExecuteCommandAsync();
            }

            //targetUserAccount.Avatar = userInfo.HeadImgUrl;
            //targetUserAccount.NickName = userInfo.NickName;
            //if (targetUserAccount.Profile == null)
            //{
            //    targetUserAccount.Profile = new UserAccountProfile();
            //}
            //targetUserAccount.Profile.Location = userInfo.Country + "," + userInfo.Country + "," + userInfo.Province;
            //await userAccountAccessor.Update(targetUserAccount);
            return targetUserAccount.Id.ToString();
        }
    }
}
