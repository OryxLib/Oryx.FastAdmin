using Microsoft.Extensions.DependencyInjection;
using Oryx.WebSocket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.WebSocket.Extension.DependencyInjection
{
    public static class OrxyWebSocketServiceExtension
    {
        public static IServiceCollection AddOryxWebSocket(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                        .AddSingleton<OryxWebSocketPool>()
                       .AddSingleton<OryxWebSocketOptions>()
                       .AddTransient<OryxWebSocketTool>();
        }
    }
}
