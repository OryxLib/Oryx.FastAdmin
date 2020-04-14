using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.ShoppingBascket;
using Oryx.ViewModel;

namespace Oryx.WxShop.Controllers
{
    public class ShoppingBascketController : Controller
    {
        public ShoppingBascketBs ShoppingBascketBs { get; set; }
        public ShoppingBascketController(ShoppingBascketBs _ShoppingBascketBs)
        {
            ShoppingBascketBs = _ShoppingBascketBs;
        }

        public async Task<IActionResult> AddBascket(Guid Id, Guid userCode)
        {
            var result = await ShoppingBascketBs.AddBascket(Id, userCode);
            return Json(result);
        }
    }
}