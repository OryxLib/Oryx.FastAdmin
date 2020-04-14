using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oryx.FastAdmin.Core3.Areas.Admin.Controllers
{
    public class ViewController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Temp()
        {
            return View();
        }

        public IActionResult Get()
        {
            return View();
        }

        public IActionResult AddOrUpdate(string modelName, Guid? Id)
        {
            if (Id == null)
            {

            }

            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}