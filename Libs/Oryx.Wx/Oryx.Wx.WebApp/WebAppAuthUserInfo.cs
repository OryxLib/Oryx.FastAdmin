using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Wx.WebApp
{
    public class WebAppAuthUserInfo
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("headimgurl")]
        public string HeadImgUrl { get; set; }

        [JsonProperty("privilege")]
        public List<string> Privilege { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
        public string AppId { get; set; }
        public string WxLoginType { get; set; } = "OfficialAccount";//OpenPlatformAccount
    }
}
