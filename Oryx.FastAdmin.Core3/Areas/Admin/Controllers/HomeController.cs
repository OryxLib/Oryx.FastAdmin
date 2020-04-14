using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Filters;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    [AdminPageRoleAuthentication("admin")]
    [Area("Admin")]
    //[Route("/Admin")]
    //[Route("/Admin/Home")]
    //[Route("/Admin/Home/Index")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}