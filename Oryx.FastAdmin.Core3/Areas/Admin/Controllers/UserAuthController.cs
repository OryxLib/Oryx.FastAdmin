using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Business;
using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core.Model;
using Oryx.FastAdmin.Core.ViewModel;
using Oryx.FastAdmin.Filters;
using Oryx.FastAdmin.Filters.Attributes;
using Oryx.FastAdmin.Model;
using Oryx.FastAdmin.Model.UserInfo;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    //[AdminPageRoleAuthentication(UserAuthBusiness.UserAuthBackendKey)]
    [Area("Admin")]
    public class UserAuthController : BaseBackendController<UserInfoEntry>
    {
        UserAuthBusiness userAuthBusiness;
        public UserAuthController(SqlSugarClient _dbClient, UserAuthBusiness _userAuthBusiness)
            : base(_dbClient)
        {
            userAuthBusiness = _userAuthBusiness;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string Password)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var result = await userAuthBusiness.Login(new Authentication.Model.UserAccount
                {
                    Password = Password,
                    UserName = userName
                });

                if (result != null)
                {
                    HttpContext.Session.SetString(UserAuthBusiness.UserAuthBackendKey, result.Id.ToString());
                    await HttpContext.Session.CommitAsync();
                }
            });

            return Json(apiMsg);
        }

        [HttpGet]
        [PageIgnore]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [PageIgnore]
        public async Task<IActionResult> Register(UserAccount userAccount)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
           {
               await userAuthBusiness.RegisterUser(userAccount);
           });
            return Json(apiMsg);
        }

        
    }
}