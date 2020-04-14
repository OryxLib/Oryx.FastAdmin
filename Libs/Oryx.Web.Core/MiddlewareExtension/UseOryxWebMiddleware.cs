using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;
using Oryx.Utilities.ValueType;
using Oryx.Web.Core.Routes;
using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.Web.Core.MiddlewareExtension
{
    public static class UseOryxWebMiddleware
    {
        static bool IsFirst = true;
        public static IApplicationBuilder UseOryxWeb(this IApplicationBuilder applicationBuilder)
        {
            if (IsFirst)
            {
                IsFirst = false;

                var staticFileTable = applicationBuilder.ApplicationServices.GetService<StaticFilesTable>();
                if (staticFileTable != null)
                {
                    foreach (var item in staticFileTable)
                    {
                        applicationBuilder.UseStaticFiles(new StaticFileOptions
                        {
                            FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item.Value.PhysicalPath))
                        });
                    }
                }
            }
            applicationBuilder.UseWebSockets();
            return applicationBuilder.Use(async (ctx, next) =>
            {
                var routeTable = ctx.RequestServices.GetService<RouteTable>();

                var serverRouteTable = ctx;//TODO : 获取服务区路由表

                var targetRoute = matchRoute(ctx, routeTable);

                if (targetRoute.Method == "ws" && ctx.WebSockets.IsWebSocketRequest)
                {
                    var websocket = await ctx.WebSockets.AcceptWebSocketAsync();
                    while (websocket.State == WebSocketState.Open)
                    {
                        var bytes = new Byte[4096];
                        var segment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        var reciver = await websocket.ReceiveAsync(segment, CancellationToken.None);
                        var handler = targetRoute.Func;
                        switch (reciver.MessageType)
                        {
                            case System.Net.WebSockets.WebSocketMessageType.Binary:
                                var streamMsg = new OryxWebContext(ctx)
                                {
                                    Stream = new MemoryStream(segment.Array)
                                };
                                streamMsg.WebSocket = websocket;
                                await handler(streamMsg);

                                break;
                            case System.Net.WebSockets.WebSocketMessageType.Close:
                                var closemsg = new OryxWebContext(ctx)
                                {
                                    Body = "Close"
                                };
                                closemsg.WebSocket = websocket;
                                await handler(closemsg);
                                break;
                            case System.Net.WebSockets.WebSocketMessageType.Text:
                                var resultMsg = Encoding.UTF8.GetString(segment.Array);
                                var msg = new OryxWebContext(ctx)
                                {
                                    Body = resultMsg
                                };
                                msg.WebSocket = websocket;
                                await handler(msg);
                                break;
                        }
                    }
                }

                if (targetRoute != null)
                {
                    var routeTableItem = targetRoute;
                    var func = routeTableItem.Func;
                    if (func != null && ctx.Request.Method.ToLower() == routeTableItem.Method)
                    {
                        var webContext = new OryxWebContext(ctx);
                        webContext.RouteValue(targetRoute.RouteValue);
                        using (webContext.HttpContext.RequestServices.CreateScope())
                        {
                            webContext.Body = await webContext.HttpContext.Request.Body.GetString();
                            if (webContext.Body.IsTrue())
                            {
                                webContext.JsonObj = JObject.Parse(webContext.Body);
                            }

                            await func(webContext);
                        }
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            });
        }

        private static RouteTableItem matchRoute(HttpContext ctx, RouteTable routeTable)
        {
            var routeTableItem = new RouteTableItem();

            foreach (var routeItem in routeTable)
            {
                var mapKey = routeItem.Key.Split('_')[0];
                var mapMethod = routeItem.Key.Split('_')[1];
                var pathBlock = ctx.Request.Path.Value.Split('/').Where(x => x.IsTrue()).ToArray();

                if (ctx.Request.Path.Value == mapKey && mapKey == "/")
                {
                    routeTableItem = routeItem.Value;
                    //匹配模板路由
                    var routeValueDictionary = Match(mapKey, ctx.Request.Path.Value);
                    if (routeValueDictionary != null)
                    {
                        routeTableItem.RouteValue = routeValueDictionary;
                    }
                    break; ;
                }

                var tmpBlock = mapKey.Split('/').Where(x => x.IsTrue()).ToArray();

                //检查路由Method
                if ((ctx.Request.Method.ToLower() != mapMethod) || ctx.WebSockets.IsWebSocketRequest && mapMethod == "ws")
                {
                    continue;
                }

                //检查路由块数量
                if (pathBlock.Length == 0 || pathBlock.Length != tmpBlock.Length)
                {
                    continue;
                }

                //检查是否匹配了模板块{xxxx}
                var matched = true;
                for (int pathIndex = 0; pathIndex < pathBlock.Length; pathIndex++)
                {
                    var pathItem = pathBlock[pathIndex];
                    var tmpItem = tmpBlock[pathIndex].ToLower();
                    if (!tmpItem.StartsWith("{") && pathItem != tmpItem)
                    {
                        matched = false;
                    }
                }

                if (matched)
                {
                    //匹配模板路由
                    var routeValueDictionary = Match(mapKey.ToLower(), ctx.Request.Path.Value.ToLower());
                    if (routeValueDictionary != null)
                    {
                        routeTableItem = routeItem.Value;
                        routeTableItem.RouteValue = routeValueDictionary;
                        break;
                    }
                }
                else
                {
                    //匹配无模板路由
                    var diff = pathBlock.Except(tmpBlock);
                    if (diff.Count() == 0)
                    {
                        routeTableItem = routeItem.Value;
                        break;
                    }
                }
            }
            return routeTableItem;
        }

        public static RouteValueDictionary Match(string routeTemplate, string requestPath)
        {
            var template = TemplateParser.Parse(routeTemplate);

            var matcher = new TemplateMatcher(template, GetDefaults(template));


            var routeValueDictionary = new RouteValueDictionary();
            if (matcher.TryMatch(requestPath, routeValueDictionary))
            {
                return routeValueDictionary;
            }
            return routeValueDictionary;
        }

        // This method extracts the default argument values from the template.
        private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }
    }
}
