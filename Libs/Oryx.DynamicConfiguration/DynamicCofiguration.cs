using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.DynamicConfiguration.Interfaces;
using System;
using System.IO;

namespace Oryx.DynamicConfiguration
{
    public class DynamicCofiguration : IDynamicCofiguration
    {
        private JObject configurationObject;
        private string configFilePath = AppContext.BaseDirectory + "oryx_config.json";
        public void SetConfigFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                configFilePath = filename;
            }
        }

        public DynamicCofiguration()
        {
            checkFileAndDir(configFilePath);
            checkJsonConfig(configFilePath);
        }

        private void checkJsonConfig(string filename)
        {
            var jsonStr = File.ReadAllText(filename);
            if (!string.IsNullOrEmpty(jsonStr))
            {
                configurationObject = JObject.Parse(jsonStr);
            }
            else
            {
                configurationObject = new JObject();
            }
        }

        private void checkFileAndDir(string filename)
        {
            var dir = Path.GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(filename);
            }

            if (!File.Exists(filename))
            {
                File.AppendAllText(filename, string.Empty);
            }
        }

        private void setjsonConfig(JObject jObject)
        {
            var jsonString = jObject.ToString();

            File.WriteAllText(configFilePath, jsonString);
        }

        public void SetConfig<T>(T model)
            where T : class, new()
        {
            var type = typeof(T);
            var type_name = type.FullName.Replace('.', '_');
            var jsonString = JsonConvert.SerializeObject(model);
            var jobject = JObject.Parse(jsonString);
            this[type_name] = jobject;
        }

        public T GetConfig<T>()
        {
            var jobject = GetConfigJToken<T>();
            return jobject.ToObject<T>();
        }

        public JToken GetConfigJToken<T>()
        {
            var type = typeof(T);
            var type_name = type.FullName.Replace('.', '_');
            return this[type_name];
        }

        public JToken this[string index]
        {
            get
            {
                return configurationObject[index];
            }
            set
            {
                configurationObject[index] = value;
                setjsonConfig(configurationObject);
            }
        }
    }
}
