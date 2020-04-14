using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Oryx.Database
{
    public static class OryxDatabaseServiceExtension
    {
        public static IServiceCollection AddOryxDbPool<T>(this IServiceCollection services, Func<T> optionsAction)
            where T : class
        {
            var dboptions = new DbOptions<T>();
            var dbPool = new OryxDatabasePool<T>(optionsAction);
            services.AddSingleton(dbPool);
            services.AddScoped<OryxDatabasePool<T>.Lease>();
            services.AddScoped(sp => sp.GetService<OryxDatabasePool<T>.Lease>().Context);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddMySql5()
                    // Set the connection string
                    .WithGlobalConnectionString("server=139.224.219.2;database=OryxFrramework;user=root;password=Linengneng123#;Character Set=utf8;")
                    // Define the assembly containing the migrations
                    //.ScanIn(typeof(AddLogTable).Assembly).For.Migrations()
                    )
               // Enable logging to console in the FluentMigrator way
               //.AddLogging(lb => lb.AddFluentMigratorConsole())
               // Build the service provider 
                .BuildServiceProvider(false);


            return services;
        }
    }
}
