using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Shop.Business.Bs.Order;

namespace Oryx.WxShop.Controllers
{
    public class OrderController : Controller
    {
        public OrderBs OrderBs { get; set; }
        public OrderController(OrderBs _OrderBs)
        {
            OrderBs = _OrderBs;
        }
        public async Task<IActionResult> OrderList(Guid userCode, int skipCount, int pageSize)
        {
            var result = await OrderBs.OrderList(userCode, skipCount, pageSize);
            return Json(result);
        }
    }
}