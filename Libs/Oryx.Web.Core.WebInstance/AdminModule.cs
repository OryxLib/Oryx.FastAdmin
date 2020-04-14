using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class AdminModule : OryxWebModule
    {
        public AdminModule()
        {
            SetStaticFiles("vueAdmin", @"OryxWeb\wwwroot\vue-element-admin-master\dist");
            Get("/Admin", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\vue-element-admin-master\dist\index.html");
            });

            SetStaticFiles("frontendBuilder", @"OryxWeb\wwwroot\frontendBuilder\");
            Get("/Admin/Page/ViewEdit", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\frontendBuilder\index2.html");
            });
        }
    }
}
