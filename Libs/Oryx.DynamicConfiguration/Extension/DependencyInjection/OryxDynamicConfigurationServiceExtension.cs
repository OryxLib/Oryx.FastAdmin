using Microsoft.Extensions.DependencyInjection;
using Oryx.DynamicConfiguration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.DynamicConfiguration.Extension.DependencyInjection
{
    public static class OryxDynamicConfigurationServiceExtension
    {
        public static IServiceCollection AddOryxDynamicConfiguration(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddSingleton<IDynamicCofiguration, DynamicCofiguration>();
            return servicesCollection;
        }
    }
}
