using Microsoft.Extensions.DependencyInjection;
using Oryx.Web.Core.Actions;
using Oryx.Web.Core.Routes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oryx.Web.Core.ServiceExtension
{
    public static class OryxWebServiceExtension
    {
        public static IServiceCollection AddOryxWeb<T>(this IServiceCollection services)
        {

            var routeTable = GetService<RouteTable>(services);
            var staticFilesTable = GetService<StaticFilesTable>(services);

            var type = typeof(T);
            var typeList = type.Assembly.GetTypes();
            //Find module and register modeul info
            var oryxWebActionList = typeList.Where(x => x.BaseType == typeof(OryxWebModule));
            foreach (var item in oryxWebActionList)
            {
                var instance = type.Assembly.CreateInstance(item.FullName);
                var moduleRouteTable = (Dictionary<string, RouteTableItem>)item.GetProperty("RouteTable").GetValue(instance);
                //Registe module route
                moduleRouteTable?.ToList()?.ForEach(routeItem =>
                {
                    if (!routeTable.ContainsKey(routeItem.Key))
                        routeTable.Add(routeItem.Key, routeItem.Value);
                });

                var moduleStaticFilesTable = (Dictionary<string, StaticFilesTableItem>)item.GetProperty("StaticFilesTable").GetValue(instance);
                //Registe module staticFilePath
                moduleStaticFilesTable?.ToList()?.ForEach(staticFilePathItem =>
                {
                    if (!staticFilesTable.ContainsKey(staticFilePathItem.Key))
                        staticFilesTable.Add(staticFilePathItem.Key, staticFilePathItem.Value);
                });

            }
            return services;
        }

        private static T GetService<T>(IServiceCollection services)
            where T : class, new()
        {
            var serviceProvider = services.BuildServiceProvider();
            var routeTable = serviceProvider.GetService<T>();
            if (routeTable == null)
            {
                routeTable = new T();
                services.AddSingleton(routeTable);
            }

            return routeTable;
        }

        private static string GetActionFullpath(Type item)
        {
            string actionFullPath = string.Empty;
            var nameSpaceList = item.Namespace.Split('.');
            var actionPath = string.Empty;
            var isInOrxyWeb = false;
            foreach (var namePath in nameSpaceList)
            {
                if (namePath == "OryxWeb")
                {
                    isInOrxyWeb = true;
                }
                if (isInOrxyWeb)
                {
                    actionPath += namePath + "/";
                }
            }
            return actionFullPath;
        }

        private static string GetTemplatePath(Type item)
        {
            string templateFullPath = GetActionFullpath(item);
            templateFullPath = templateFullPath + item.Name + ".html";
            return templateFullPath;
        }
    }
}
