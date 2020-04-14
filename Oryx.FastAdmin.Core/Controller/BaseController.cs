using Oryx.FastAdmin.Model.BusinessModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
namespace Oryx.FastAdmin.Core.Controller
{
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        SqlSugarClient dbClient { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public BaseController(SqlSugarClient _dbClient)
        {
            dbClient = _dbClient;
            Config = new Dictionary<string, string>();
            InitBaseController();
        }

        private void InitBaseController()
        {
            var configEntiry = dbClient.Queryable<ConfigEntry>().First();

            MapDataToDctionary<ConfigEntry>(configEntiry);
        }

        private void MapDataToDctionary<T>(object data)
        {
            if (data == null)
            {
                return;
            }
            //var newDictionary = new Dictionary<string, string>();
            var _type = typeof(T);
            var properties = _type.GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(data).ObjToString();
                var name = prop.Name;
                Config.Add(name, value);
            }
        }
    }
}
