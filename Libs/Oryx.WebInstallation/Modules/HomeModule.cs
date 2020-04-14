using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.WebInstallation.Modules
{
    public class HomeModule : OryxWebModule
    {
        public HomeModule()
        {
            SetStaticFiles("frontenddesign", @"OryxWeb");

            Get("/install", async ctx =>
            {
                await ctx.Render(@"OryxWeb\Home\Index.html");
            });

            Get("/install/test", async ctx =>
            {
                //await ctx.Render(@"OryxWeb\Home\test.html");
                await ctx.RenderWithLayout(@"OryxWeb\Home\test.html", @"OryxWeb\Home\Index.html");
            });

        }
    }
}
