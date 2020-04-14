using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.Goods;
using Oryx.ViewModel;

namespace Oryx.WxShop.Controllers
{
    public class GoodsController : Controller
    {
        public GoodsBs GoodsBs { get; set; }
        public GoodsController(GoodsBs _GoodsBs)
        {
            GoodsBs = _GoodsBs;
        }

        public async Task<IActionResult> GoodsCategory()
        {
            var result = await GoodsBs.ListGoodCategory();
            return Json(result);
        }

        public async Task<IActionResult> GoodsList()
        {
            var result = await GoodsBs.ListGoods(0, 0);
            return Json(result);
        }

        public async Task<IActionResult> GoodsDetail(Guid Id)
        {
            var result = await GoodsBs.GetInfo(Id);
            return Json(result);
        }
    }
}