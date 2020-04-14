
using Microsoft.Extensions.DependencyInjection;
using Oryx.FastAdmin.Core.DatabaseOperation;
using System;

namespace Oryx.FastAdmin.Core.DatabaseOperation
{
    public static class OryxDatabaseServiceExtension
    {
        public static IServiceCollection AddOryxDbPool<T>(this IServiceCollection services, Func<T> optionsAction)
            where T : class
        {
            //var dboptions = new DbOptions<T>();
            var dbPool = new OryxDatabasePool<T>(optionsAction);
            //var lease = new OryxDatabasePool<T>.Lease(dbPool);
            services.AddSingleton(dbPool);
            services.AddScoped<OryxDatabasePool<T>.Lease>( );
            services.AddScoped(sp => sp.GetService<OryxDatabasePool<T>.Lease>().Context);

            return services;
        }
    }
}
