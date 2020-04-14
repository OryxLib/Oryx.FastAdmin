using Oryx.Wx.Core;
using Oryx.Wx.Core.Utitlities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Wx.MiniApp
{
    public class WxaCode
    {
        private WxAccessToken _accessToken;
        public WxaCode(WxAccessToken accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<Stream> GetWxaCode(string voteType)
        {
            return await HttpRequest.Post($"https://api.weixin.qq.com/wxa/getwxacode?access_token={_accessToken.GetAccessToken()}", new { page = "/page/index/index?type=" + voteType });
        }


    }
}
