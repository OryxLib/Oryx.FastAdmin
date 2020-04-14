using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oryx.FastAdmin.Core.StateManage;
using Oryx.FastAdmin.States;
using SqlSugar;
using Oryx.FastAdmin.Core.DatabaseOperation;
using Oryx.FastAdmin.Model;
using Oryx.FastAdmin.Core.Model;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http.Features;
using Oryx.Authentication.Business;
using Oryx.Authentication.Model;
using Oryx.Web.Core.MiddlewareExtension;
using Oryx.WebSocket.Extension.Builder;
using Oryx.WebSocket.Extension.DependencyInjection;
using Oryx.FastAdmin.Core3.WebSocketHandler;
using Oryx.Web.Core.DataModel;
using Oryx.Web.Core.ServiceExtension;
using Oryx.Web.Core.WebInstance;
using Oryx.Wx.JsSdk;
using Oryx.Wx.Core;
using Microsoft.AspNetCore.StaticFiles;
using Hangfire;
using Hangfire.MySql.Core;
using Oryx.FastAdmin.Core.BusinessModel;
using Oryx.FastAdmin.Core3.ApplicationService;
using Oryx.DynamicConfiguration.Extension.DependencyInjection;
using Oryx.WebInstallation.Extension.Services;
using Oryx.WebInstallation.Extension.Middlewares;
using Oryx.SNS;

namespace Oryx.FastAdmin.Core3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   //install-package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            services.AddServerSideBlazor();

            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(x =>
                {

                });

            services.AddOryxDbPool<SqlSugarClient>(() =>
            {
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig
                {
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = "mysqlconnection"
                    //ConnectionString = "Server=47.103.143.47;Database=edu_pagetechs;User=edu_pagetechs;Password=5NaYzGnRD7XEDtXJ;Character Set=utf8;"
                    //ConnectionString = "server=47.103.143.47;uid=edu_pagetechs;pwd=5NaYzGnRD7XEDtXJ;database=edu_pagetechs"
                });
                //SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                //{
                //    DbType = DbType.Sqlite,
                //    ConnectionString = "DataSource=" + AppDomain.CurrentDomain.BaseDirectory + "maindb.sqlite",
                //    InitKeyType = InitKeyType.Attribute,
                //    IsAutoCloseConnection = true,
                //    AopEvents = new AopEvents
                //    {
                //        OnLogExecuting = (sql, p) =>
                //        {
                //            Console.WriteLine(sql);
                //            Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));
                //        }
                //    }
                //});
                //If no exist create datebase 
                //db.DbMaintenance.
                db.DbMaintenance.CreateDatabase();

                //�Զ��������ݿ�
                var types = ModelFactory.GetAllTypes().Where(x => x.BaseType == typeof(BaseModel));
                var userAuthTypes = UserAuthModelFactory.GetAllTypes().Where(x => x.BaseType == typeof(BaseModel));
                var webcoreModel = DataModelModelFactory.GetAllTypes().Where(x => x.BaseType == typeof(BaseModel));
                var corModel = CoreModelFactory.GetAllTypes().Where(x => x.BaseType == typeof(BaseModel));
                var allTypes = types.Concat(userAuthTypes);
                allTypes = allTypes.Concat(webcoreModel);
                allTypes = allTypes.Concat(corModel);
                foreach (var type in allTypes)
                {
                    db.CodeFirst.InitTables(type);
                }

                return db;
            });


            services.AddSession();
            services.AddSingleton<ISessionFeature>(new DefaultSessionFeature());
            services.AddDistributedMemoryCache();
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "101.132.130.133:6379,password=Linengneng123#";
            //    option.InstanceName = "master";
            //});

            //var redis = ConnectionMultiplexer.Connect("101.132.130.133:6379,password=Linengneng123#");
            //services.AddDataProtection()
            // .SetApplicationName("session_application_name")
            // .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");
            //.PersistKeysToRedis(redis, "DataProtection-Keys"); 

            services.AddHangfire(x => x.UseStorage(new MySqlStorage("mysqlconnection",
                new MySqlStorageOptions()
                {
                    TransactionIsolationLevel = System.Data.IsolationLevel.ReadCommitted,
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 50000,
                    TransactionTimeout = TimeSpan.FromMinutes(1),
                    TablePrefix = "Hangfire_"
                })));
            services.AddHangfireServer();

            //services.AddOryxWeb<Bootstrapper>();

            services.AddOryxWebSocket();
            services.AddSingleton<ChatHandler>();
            #region 
            services.AddSingleton<StateHub>();
            services.AddTransient<AuthStateProcessor>();
            services.AddTransient<UserAuthBusiness>();
            services.AddTransient<CommentApplicationService>();
            services.AddTransient<UserApplicationService>();

            //��̬�����ļ�ע��
            services.AddOryxDynamicConfiguration();

            //ע��oryxWebModeul
            services.AddOryxWeb<SNSModule>();
            services.AddOryxWebInstall();

            #endregion
            //΢�Ź��Ŵ�Ԫ߹��ע��
            services.AddSingleton(new WxAccessToken(Configuration["wx:appId"], Configuration["wx:secret"]));
            services.AddSingleton<Jsapi_Ticket>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            var staticFilesOptions = new StaticFileOptions();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".py", "application/octet-stream");
            staticFilesOptions.ContentTypeProvider = provider;

            app.UseStaticFiles(staticFilesOptions);
            app.UseSession();

            //app.UseCors(option =>
            //{
            //    option.WithOrigins("upload-z1.qiniup.com");
            //    option.WithOrigins("localhost:8601");
            //    //option.AllowAnyOrigin();
            //    //option.
            //    option.AllowAnyHeader();
            //    option.AllowAnyMethod();
            //});

            app.UseHangfireDashboard();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 50,
            });
            //BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            //BackgroundJob.Schedule(() => Console.WriteLine("Hello, world"),
            //            TimeSpan.FromMinutes(1));
            //RecurringJob.AddOrUpdate(() => Console.Write("Easy!"), Cron.Daily);

            app.Use((ctx, next) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", ctx.Request.Headers["Origin"]);
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", ctx.Request.Headers["Access-Control-Request-Method"]);
                ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "AccessToken,Content-Type");
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");
                if (ctx.Request.Method.ToLower() == "options")
                {
                    ctx.Response.StatusCode = 204;

                    return Task.CompletedTask;
                }

                var sessionFeature = ctx.RequestServices.GetService<ISessionFeature>();// 
                sessionFeature.Session = ctx.Session;
                return next();
            });

            app.UseOryxWeb();
            app.UseOryxWebInstall(option =>
            {

            });


            app.UserOryxWebSocket(action =>
            {
                action.Register("/chat", serviceProvider.GetService<ChatHandler>());
            });

            app.UseRouting();

            app.UseAuthorization();

            //new StartupRegister(app, env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                 name: "areas",
                 areaName: "Admin",
                 pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapAreaControllerRoute(
                name: "areas2",
                areaName: "Scratch",
                pattern: "Scratch/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "areas3",
                   areaName: "Installation",
                   pattern: "Installation/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "areas4",
                   areaName: "Python",
                   pattern: "Python/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "areas5",
                   areaName: "TaskSchedule",
                   pattern: "TaskSchedule/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "areas6",
                   areaName: "UserAdmin",
                   pattern: "UserAdmin/{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
