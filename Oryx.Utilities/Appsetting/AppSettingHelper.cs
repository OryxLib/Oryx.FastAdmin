using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oryx.Utilities.Appsetting
{
    public static class AppSettingHelper
    {
        public static void AddOrUpdateAppSetting<T>(string key, T value, string configFileName = "appSettings.json")
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, configFileName);
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var sectionPath = key.Split(':')[0];
                if (!string.IsNullOrEmpty(sectionPath))
                {
                    var keyPath = key.Split(':')[1];
                    jsonObj[sectionPath][keyPath] = value;
                }
                else
                {
                    jsonObj[sectionPath] = value; // if no sectionpath just set the value
                }
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);

            }
            catch (Exception)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
