using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oryx.WxShop.Controllers
{
    public class WxAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}