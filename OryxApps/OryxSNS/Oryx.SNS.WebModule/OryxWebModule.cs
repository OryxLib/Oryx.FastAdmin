using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.SNS
{
    public partial class SNSModule : OryxWebModule
    {
        public SNSModule()
        {
            SetStaticFiles("frontenddesign", @"OryxWeb");

            Get("/sns", async ctx =>
            {
                //await ctx.Render(@"OryxWeb\Home\index.html");
                await ctx.RenderWithLayout(@"OryxWeb\Home\index.html", @"OryxWeb\Home\layout.html");
            });

        }
    }
}
