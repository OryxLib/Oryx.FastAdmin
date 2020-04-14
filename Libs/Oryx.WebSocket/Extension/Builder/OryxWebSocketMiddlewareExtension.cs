using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Oryx.WebSocket.Infrastructure;
using Oryx.WebSocket.Interface;
using System;
using System.Collections.Generic;
//using Oryx.VoteApp.Server.WebSocketHandler;

namespace Oryx.WebSocket.Extension.Builder
{
    public static class OryxWebSocketMiddlewareExtension
    {
        public static IApplicationBuilder UserOryxWebSocket(this IApplicationBuilder app, Action<OryxWebSocketOptions> optionHandler)
        {
            var webSocketPool = app.ApplicationServices.GetService<OryxWebSocketPool>();

            var oryxWsTool = app.ApplicationServices.GetService<OryxWebSocketTool>();

            var option = app.ApplicationServices.GetService<OryxWebSocketOptions>();

            optionHandler(option);

            return app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = new TimeSpan(0, 5, 0)
            }).MapWhen(ctx =>
            {
                return option.OptionsDic.ContainsKey(ctx.Request.Path) && ctx.WebSockets.IsWebSocketRequest;
            }, wsApp =>
            {
                wsApp.Use(async (ctx, next) =>
                {
                    var k = ctx.Request.Path;
                    var v = option.OptionsDic[ctx.Request.Path];
                    var kvp = new KeyValuePair<string, IOryxHandler>(k, v);
                    await oryxWsTool.ProcessWebSocket(ctx, kvp);
                });
            });
        }
    }
}
