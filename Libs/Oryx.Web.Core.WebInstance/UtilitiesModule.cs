using Oryx.Utilities;
using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class UtilitiesModule : OryxWebModule
    {
        public UtilitiesModule()
        {

            Get("/Image/Token", async ctx =>
            {
                var token = QiniuTool.GenerateToken();
                await ctx.Ajax(new { token });
            });
        }
    }
}
