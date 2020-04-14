using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class InstanceBDModule : OryxWebModule
    {
        public InstanceBDModule()
        {
            SetStaticFiles("BD", @"OryxWeb\wwwroot\backendBuilder\dist");


            Get("/bd", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\backendBuilder\dist\index.html");
            });

            Get("/bd/create", async ctx =>
            {

            });
        }
    }
}
