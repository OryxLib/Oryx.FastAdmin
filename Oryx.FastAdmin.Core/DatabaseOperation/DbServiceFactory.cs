using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oryx.FastAdmin.Core.DatabaseOperation
{
    public static class DbServiceFactory
    {
        public static IServiceCollection AddSocialBusiness(this IServiceCollection services, string fileName)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.Sqlite,
                ConnectionString = AppDomain.CurrentDomain.BaseDirectory + fileName + ".sqlite",
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                AopEvents = new AopEvents
                {
                    OnLogExecuting = (sql, p) =>
                    {
                        Console.WriteLine(sql);
                        Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));
                    }
                }
            });
            //If no exist create datebase 
            db.DbMaintenance.CreateDatabase();

            //services.AddTransient (db);

            return services;
        }
    }
}
