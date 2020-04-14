using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Wx.Core.Utitlities
{
    public class HttpRequest
    {
        public static async Task<string> Get(string url)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Timeout = 30000;
                var res = await webRequest.GetResponseAsync();
                var resStream = res.GetResponseStream();
                var sr = new StreamReader(resStream);
                return await sr.ReadToEndAsync();
            }
            catch (Exception exc)
            {
                return exc.Message;
            }

            //var wc = new WebClient();
            //wc.GetWebRequest
            //return await wc.DownloadStringTaskAsync(url);
        }

        public static async Task<Stream> Post(string url, object jsonObj)
        {
            var wc = new WebClient();
            var jsonObjStr = JsonConvert.SerializeObject(jsonObj);
            var bytes = Encoding.UTF8.GetBytes(jsonObjStr);
            var resBytes = await wc.UploadDataTaskAsync(url, bytes);
            var stream = new MemoryStream(bytes);
            return stream;
        }

        public static async Task<Stream> Post(string url, string content)
        {
            var wc = new WebClient();
   
             var bytes = Encoding.UTF8.GetBytes(content);
            var resBytes = await wc.UploadDataTaskAsync(url, bytes);
            var stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
