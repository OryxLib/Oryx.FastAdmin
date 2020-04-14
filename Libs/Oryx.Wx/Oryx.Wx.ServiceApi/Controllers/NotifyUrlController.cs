using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oryx.Wx.ServiceApi.Controllers
{
    public class NotifyUrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}