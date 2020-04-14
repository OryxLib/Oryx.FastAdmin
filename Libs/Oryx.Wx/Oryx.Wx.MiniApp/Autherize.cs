using Oryx.Wx.Core.Utitlities;
using System;
using System.Threading.Tasks;

namespace Oryx.Wx.MiniApp
{
    public class Autherize
    {
        public static async Task<string> CheckSession(string appid, string secret, string code)
        {
            return await HttpRequest.Get($"https://api.weixin.qq.com/sns/jscode2session?appid={appid}&secret={secret}&js_code={code}&grant_type=authorization_code");
        }
    }
}
