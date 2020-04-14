using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Oryx.FastAdmin.Core3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(
                        "appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // .UseHttpSys(options =>
                    //{
                    //    options.MaxAccepts = 65535;
                    //    options.MaxConnections = -1;
                    //    options.RequestQueueLimit = 65535;
                    //});
                    //.UseKestrel(options =>
                    //{
                    //    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(30);
                    //    options.Limits.MaxConcurrentConnections = null;
                    //    options.Limits.MaxConcurrentUpgradedConnections = null;
                    //    options.Limits.MaxRequestBodySize = null;
                    //    options.Limits.MaxRequestBufferSize = null;
                    //    options.Limits.MaxRequestLineSize = int.MaxValue;
                    //    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                    //});
                });
    }
}
