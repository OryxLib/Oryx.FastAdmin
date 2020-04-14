using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.CLI
{
    public static class CLIServiceFactory
    {
        public static IServiceCollection AddUserAccountBusiness(this IServiceCollection services)
        {
            services
                .AddTransient<ConfigHelper>();
            return services;
        }
    }
}
