using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class WsModule : OryxWebModule
    {
        public WsModule()
        {
            Get("/testws", async ctx =>
            {
                await ctx.Render(AppDomain.CurrentDomain.BaseDirectory + "OryxWeb/Index/testws.html");
            });
            WS("/ws", async ctx =>
            {
                await ctx.Send("哈哈哈");
            });
        }
    }
}