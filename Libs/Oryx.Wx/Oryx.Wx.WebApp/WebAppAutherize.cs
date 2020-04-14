using Newtonsoft.Json;
using Oryx.Wx.Core.Utitlities;
using System;
using System.Threading.Tasks;

namespace Oryx.Wx.WebApp
{
    public class WebAppAutherize
    {
        public static async Task<UserAccessTokenResult> GetUserAccessToken(string appId, string appsecret, string code)
        {
            var resultStr = await HttpRequest.Get($"https://api.weixin.qq.com/sns/oauth2/access_token?appid={appId}&secret={appsecret}&code={code}&grant_type=authorization_code");
            return JsonConvert.DeserializeObject<UserAccessTokenResult>(resultStr);
        }

        public static async Task<UserAccessTokenResult> RefreshUserAccessToken(string appId, string refreshtoken)
        {
            var resultStr = await HttpRequest.Get($"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={appId}&grant_type=refresh_token&refresh_token={refreshtoken}");
            return JsonConvert.DeserializeObject<UserAccessTokenResult>(resultStr);
        }

        public static async Task<WebAppAuthUserInfo> GetUserInfo(string accessToken, string openId)
        {
            var resultStr = await HttpRequest.Get($"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={openId}&lang=zh_CN");
            return JsonConvert.DeserializeObject<WebAppAuthUserInfo>(resultStr);
        }
    }
}
