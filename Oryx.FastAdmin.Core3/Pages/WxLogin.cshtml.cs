using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Oryx.FastAdmin.Core3.Pages
{
    public class WxLoginModel : PageModel
    {
        private readonly IConfiguration configuration;

        public string GotoUrl { get; set; }

        public WxLoginModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public void OnGet(string url, string scope = "snsapi_userinfo")
        {
            var redirectUrlWrap = configuration["Ciyuanya:Host"] + $"/Auth/WxLogin?url={url}";
            GotoUrl = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={configuration["Ciyuanya:Wx:WebAppId"]}&redirect_uri={ WebUtility.UrlEncode(redirectUrlWrap)}&response_type=code&scope={scope}&state=123#wechat_redirect";
        }
    }
}