using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Filters;

namespace Oryx.FastAdmin.Core3.Controllers
{
    [PageRoleAuthentication("user")]
    public class UserInfoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}