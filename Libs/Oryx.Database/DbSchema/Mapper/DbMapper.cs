
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oryx.Database.DbSchema.Mapper
{
    /// <summary>
    /// Json DB Schema Mapper to Class
    /// </summary>
    public class DbMapper
    {
        public DataTable Load(string path)
        {
            var fileTxt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Resource\\" + path);
            return JsonConvert.DeserializeObject<DataTable>(fileTxt);
        }
    }
}
