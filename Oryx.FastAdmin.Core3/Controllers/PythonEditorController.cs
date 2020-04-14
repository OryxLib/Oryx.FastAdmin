using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class PythonEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }     
        public IActionResult Sample()
        {
            return View();
        }
    }
}