using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Oryx.DynamicConfiguration.Extension.DependencyInjection;
using Oryx.DynamicConfiguration.Interfaces;
using Oryx.Web.Core.ServiceExtension;
using Oryx.WebInstallation.Modules;

namespace Oryx.WebInstallation.Extension.Services
{
    public static class OryxWebInstallServiceExtension
    {
        public static IServiceCollection AddOryxWebInstall(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddOryxWeb<HomeModule>();
            var configService = servicesCollection.BuildServiceProvider().GetService<IDynamicCofiguration>();
            if (configService == null)
            {
                servicesCollection.AddOryxDynamicConfiguration();
            }

            return servicesCollection;
        }
    }
}
