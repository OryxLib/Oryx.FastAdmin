using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oryx.Utilities.Appsetting;

namespace Oryx.FastAdmin.Core3.Areas.Python.Controllers
{
    [Area("Python")]
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

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult SetConfig()
        {
            AppSettingHelper.AddOrUpdateAppSetting("", "", "config.json");
            return Json(new { });
        }
    }
}