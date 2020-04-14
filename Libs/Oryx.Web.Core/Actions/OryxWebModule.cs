using Oryx.Web.Core.Routes;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Oryx.Web.Core.Actions
{
    public class OryxWebModule
    {
        public OryxWebModule()
        {
            RouteTable = new RouteTable();
            StaticFilesTable = new StaticFilesTable();
        }

        public IServiceProvider ServiceProvider { get; }

        public RouteTable RouteTable { get; set; }

        public StaticFilesTable StaticFilesTable { get; set; }

        public string UserId { get; set; }

        public string UserIdGuid { get; set; }

        public void Auth()
        {

        }

        public void Cache(string key, object data)
        {

        }

        public void Localization(string key)
        {

        }

        public void Lan(string key) => Localization(key);

        public void L(string key) => Localization(key);

        public void SetStaticFiles(string name, string physicalPath, string requestPath = "")
        {
            if (!StaticFilesTable.Any(x => x.Key != name && x.Value.PhysicalPath == physicalPath))
            {
                StaticFilesTable.Add(name, new StaticFilesTableItem
                {
                    PhysicalPath = physicalPath,
                    RequestPath = requestPath
                });
            }
        }

        public T GetType<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public void Get(string route, Func<OryxWebContext, Task> func)
        {
            Request(route, "get", func);
        }

        public void Post(string route, Func<OryxWebContext, Task> func)
        {
            Request(route, "post", func);
        }

        public void Request(string route, string method, Func<OryxWebContext, Task> func)
        {
            RouteTable.Add(route + "_" + method, new RouteTableItem
            {
                Func = func,
                Path = route,
                Method = method
            });
        }

        public void WS(string route, Func<OryxWebContext, Task> func)
        {
            RouteTable.Add(route + "_ws", new RouteTableItem
            {
                Func = func,
                Path = route,
                Method = "ws"
            });
        }

        //Tool

        public string LoadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
