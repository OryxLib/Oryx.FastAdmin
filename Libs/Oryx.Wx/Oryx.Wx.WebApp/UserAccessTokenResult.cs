using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Wx.WebApp
{
    public class UserAccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("scope")]
        public string socpe { get; set; }
    }
}
