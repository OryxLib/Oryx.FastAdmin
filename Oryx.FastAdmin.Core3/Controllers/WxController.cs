using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.Utilities;
using Oryx.Wx.Pay.WxPayAPI;
using Oryx.Utilities.Redis;
using Oryx.Wx.JsSdk;
using Oryx.Wx.WebApp;
using Microsoft.Extensions.Configuration;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class WxController : Controller
    {
        private readonly IDistributedCache distributedCache;
        private readonly Jsapi_Ticket jsapi_Ticket;
        private readonly IConfiguration configuration;
        public WxController(IDistributedCache _distributedCache,
            IConfiguration _configuration,
            Jsapi_Ticket _jsapi_Ticket)
        {
            distributedCache = _distributedCache;
            jsapi_Ticket = _jsapi_Ticket;
            configuration = _configuration;
        }

        public async Task<IActionResult> JsApiPackage(string url)
        {
            string AppId = configuration["wx:appId"];//"wx568e40eaad7d6736";
            string AppSecret = configuration["wx:secret"];

            url = Uri.UnescapeDataString(url.Trim());
            string timestamp = TimeStamp.Get().ToString();//生成签名的时间戳
            string nonceStr = WxPayApi.GenerateNonceStr();//生成签名的随机串
            //string url = url;//当前的地址
            string jsapi_ticket = "";
            //ticket 缓存7200秒
            var jsapi_ticket_cache = await distributedCache.GetValue<string>("jsapi_ticket");
            if (jsapi_ticket_cache != null && !jsapi_ticket_cache.IsExpired)
            {
                jsapi_ticket = jsapi_ticket_cache.Value;
            }
            else
            {
                var jsapi_ticket_jstr = await jsapi_Ticket.Get();//{"errcode":0,"errmsg":"ok","ticket":"HoagFKDcsGMVCIY2vOjf9j2_hOCCXe2SnD-zgYvJabyCVaPRPj2H8mQ0E3G_WEDY_Jj4YtnRnU1iELVemAwt2g","expires_in":7200}
                var jobj = Newtonsoft.Json.Linq.JObject.Parse(jsapi_ticket_jstr);
                jsapi_ticket = jobj["ticket"]?.ToString();
                await distributedCache.SetValue(new RedisDocument<string>
                {
                    ExpireTime = DateTime.Now.AddSeconds(7200),
                    Key = "jsapi_ticket",
                    SetTime = DateTime.Now,
                    Value = jsapi_ticket
                });
            }

            string[] ArrayList = { "jsapi_ticket=" + jsapi_ticket, "timestamp=" + timestamp, "noncestr=" + nonceStr, "url=" + url };
            Array.Sort(ArrayList);
            string signature = string.Join("&", ArrayList);
            // signature = FormsAuthentication.HashPasswordForStoringInConfigFile(signature, "SHA1").ToLower();

            var sha1 = System.Security.Cryptography.SHA1.Create();

            byte[] buffer = Encoding.UTF8.GetBytes(signature);
            var hash = sha1.ComputeHash(buffer);
            //var hashString = Convert.ToBase64String(hash).ToLower();
            var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
            //string addrSign = FormsAuthentication.HashPasswordForStoringInConfigFile(param, "SHA1");
            return Content("{\"appId\":\"" + AppId + "\", \"timestamp\":" + timestamp + ",\"nonceStr\":\"" + nonceStr + "\",\"signature\":\"" + hashString + "\"}", "application/Json");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}