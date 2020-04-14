using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Utilities.Redis
{
    public static class IDistributeCacheExtension
    {
        public static async Task SetValue<T>(this IDistributedCache cache, RedisDocument<T> document)
        {
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var valueStr = JsonConvert.SerializeObject(document, jsonSetting);
            await cache.SetStringAsync(document.Key, valueStr);
        }

        public static async Task<RedisDocument<T>> GetValue<T>(this IDistributedCache cache, string key)
        {
            var valueString = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(valueString))
            {
                return null;
            }
            var valueObj = JsonConvert.DeserializeObject<RedisDocument<T>>(valueString);
            return valueObj;
        }
    }
}
