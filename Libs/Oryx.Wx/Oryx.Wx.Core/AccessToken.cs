 using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Wx.Core
{
    public class WxAccessToken
    {
        static bool needUpdate = false;
        static string tokenCache = "";
        static object threadBlock = new Object();
        private readonly string appKey;
        private readonly string Secret;
        public WxAccessToken(string _appKey, string _Secret)
        {
            appKey = _appKey;
            Secret = _Secret;
        }

        public string GetAccessToken()
        {
            var cashFile = AppDomain.CurrentDomain.BaseDirectory + $"tokencash{ appKey}.txt";
            lock (threadBlock)
            {
                if (!File.Exists(cashFile))
                {
                    needUpdate = true;
                }
                else
                {
                    var createTime = File.GetCreationTime(cashFile);
                    if (createTime.AddMinutes(2) < DateTime.Now)
                    {
                        needUpdate = true;
                    }
                }

                var accessToken = "";
                if (needUpdate)
                {
                    WebClient wc = new WebClient();
                    var resultJson = wc.DownloadString($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appKey}&secret={Secret}");
                    var resultJobj = JObject.Parse(resultJson);
                    if (resultJobj["access_token"] != null)
                    {
                        tokenCache = accessToken = resultJobj["access_token"].ToString();
                        File.Delete(cashFile);
                        File.WriteAllText(cashFile, accessToken);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tokenCache))
                    {
                        return tokenCache;
                    }
                    tokenCache = accessToken = File.ReadAllText(cashFile);
                }

                return accessToken;
            }
        }
    }
}
