using Microsoft.AspNetCore.Builder;
using Oryx.Web.Core.MiddlewareExtension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.WebInstallation.Extension.Middlewares
{
    public static class OryxWebInstallMiddlewareExtension
    {
        public static IApplicationBuilder UseOryxWebInstall(this IApplicationBuilder applicationBuilder
            , Action<OryxWebInstallOptions> optionHandler)
        {
            applicationBuilder.UseOryxWeb(); 

            return applicationBuilder;
        }
    }
}
