using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oryx.Authentication.Business;
using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core.ViewModel;
using Oryx.FastAdmin.Core3.Models;
using Oryx.Utilities.Redis;
using Oryx.Wx.WebApp;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class UserAuthController : FrontendBaseController
    {
        SqlSugarClient dbClient;
        UserAuthBusiness userAuthBusiness;
        IConfiguration configuration;
        private string WxAppId = string.Empty;
        private string WxAppSecret = string.Empty;

        public UserAuthController(SqlSugarClient _dbClient
            , UserAuthBusiness _userAuthBusiness,
            IConfiguration _configuration) : base(_dbClient)
        {
            dbClient = _dbClient;
            userAuthBusiness = _userAuthBusiness;
            configuration = _configuration;
            WxAppId = _configuration["wx:appId"];
            WxAppSecret = _configuration["wxsecret"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var result = await userAuthBusiness.Login(new UserAccount
                {
                    Password = password,
                    UserName = userName
                });

                if (result != null)
                {
                    HttpContext.Session.SetString(UserAuthBusiness.UserAuthFrontendKey, result.Id.ToString());
                    await HttpContext.Session.CommitAsync();
                }
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> Logout(string userToken)
        {

            var apiMsg = await ApiMessage.Wrap(async () =>
            {

                HttpContext.Session.Remove(UserAuthBusiness.UserAuthFrontendKey);

                await userAuthBusiness.LogOut(userToken);
            });

            return Json(apiMsg);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var userAccount = new UserAccount
                {
                    Id = Guid.NewGuid(),
                    UserName = registerViewModel.UserName,
                    Password = registerViewModel.Password
                };
                await userAuthBusiness.RegisterUser(userAccount);

                if (userAccount != null)
                {
                    HttpContext.Session.SetString(UserAuthBusiness.UserAuthFrontendKey, userAccount.Id.ToString());
                    await HttpContext.Session.CommitAsync();
                }
            });

            return Json(apiMsg);
        }

        public IActionResult Reset()
        {
            return View();
        }

        public async Task<IActionResult> Reset(ResetViewModel resetViewModel)
        {
            return View();
        }

        public async Task<IActionResult> WxWebLogin(string code, string url, string state)
        {
            var accessTokenResult = await WebAppAutherize.GetUserAccessToken(WxAppId, WxAppSecret, code);
            //await distributedCache.SetValue(new RedisDocument<UserAccessTokenResult>
            //{
            //    ExpireTime = DateTime.Now.AddSeconds(accessTokenResult.ExpiresIn),
            //    Key = WxAppId + "_" + accessTokenResult.OpenId + "_AccessToken",
            //    SetTime = DateTime.Now,
            //    Value = accessTokenResult
            //});
            var userInfo = await WebAppAutherize.GetUserInfo(accessTokenResult.AccessToken, accessTokenResult.OpenId);
            userInfo.AppId = configuration["wx:appId"];
            var wxLoginUserId = await userAuthBusiness.WxWebLoin(userInfo);
            //将用户Id提交到Redis 缓存

            HttpContext.Session.Set(UserAuthBusiness.UserAuthFrontendKey, Encoding.UTF8.GetBytes(wxLoginUserId));
            await HttpContext.Session.CommitAsync();
            await SetAuth(wxLoginUserId);

            return Redirect(url);
        }

        private async Task SetAuth(string userId)
        {
            var accessToken = Guid.NewGuid().ToString();
            //await distributedCache.SetStringAsync(accessToken, userId);
            HttpContext.Response.Headers["AccessToken"] = accessToken;
        }
    }
}