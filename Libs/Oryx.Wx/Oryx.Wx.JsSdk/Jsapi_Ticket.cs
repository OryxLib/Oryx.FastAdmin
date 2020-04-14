using Oryx.Wx.Core;
using Oryx.Wx.Core.Utitlities;
using System;
using System.Threading.Tasks;

namespace Oryx.Wx.JsSdk
{
    public class Jsapi_Ticket
    {
        private WxAccessToken _accessToken;
        public Jsapi_Ticket(WxAccessToken accessToken)
        {
            _accessToken = accessToken;
        }
        public async Task<string> Get()
        {
            return await HttpRequest.Get("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + _accessToken.GetAccessToken() + "&type=jsapi");
        }

    }
}
