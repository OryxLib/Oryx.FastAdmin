using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Web.Core.Routes
{
    public static class RequestExtension
    {
        public static string GetValue(this HttpRequest request, string paramKey)
        {
            return request.Query[paramKey];
        }

        public static async Task<T> PostValue<T>(this HttpRequest request, string paramKey)
        {
            if (typeof(T).IsClass)
            {
                var bodyStreamReader = new StreamReader(request.Body);
                var bodyStr = await bodyStreamReader.ReadToEndAsync();
                JsonSerializerSettings setting = new JsonSerializerSettings();
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return JsonConvert.DeserializeObject<T>(bodyStr, setting);
            }
            else
            {
                return (T)Convert.ChangeType(request.Query[paramKey], typeof(T));
            }
        }
    }
}
