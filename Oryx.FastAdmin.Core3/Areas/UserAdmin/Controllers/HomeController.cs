using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Filters;
using Oryx.Utilities.Appsetting;

namespace Oryx.FastAdmin.Core3.Areas.UserAdmin.Controllers
{
    [Area("UserAdmin")]
    [PageRoleAuthentication("user")]
    public class HomeController : Controller
    {
        IConfiguration configuration;
        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult Index()
        {
            configuration["test"] = "test2";
            return View();
        }

        public IActionResult SetConfig()
        {
            AppSettingHelper.AddOrUpdateAppSetting("", "", "config.json");
            return Json(new { });
        }
    }
}