using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Utilities.QRCodeTool;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class QRcodeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string path)
        {
            var qrcode = QRCodeHelper.Generate(path);
            var base64 = QRCodeHelper.BitMapToBase64(qrcode);
            return Content(base64);
        }
    }
}