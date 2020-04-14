using Oryx.Web.Core.Actions;
using Oryx.Web.Core.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Index
{
    public class IndexHandler : OryxWebModule
    {
        public IndexHandler()
        {
            SetStaticFiles("indexModule", @"OryxWeb\wwwroot");

            Get("/", async ctx =>
            {
                await ctx.Render(@"OryxWeb\Index\Index.html");
            });

            Get("/test2", async ctx =>
            {
                await ctx.Ajax(new { param = ctx["a"], teststs = 111 });
            });

            Get("/test2/{subpath}", async ctx =>
            {
                await ctx.Ajax(new { path = ctx["subpath"], param = ctx["a"], teststs = 111 });
            });

            Get("/test2/aaa", async ctx =>
            {
                await ctx.Ajax(new { path = "我是真aaa", param = ctx["a"], teststs = 111 });
            });
        }
    }
}
