using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using Newtonsoft.Json.Linq;
using Oryx.Database;
using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class Bootstrapper : OryxWebModule
    {
        public Bootstrapper()
        {
            Get("/boot", async ctx =>
            {
                var initdbJson = JObject.Parse(LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Json/InitDb.json"));
                var initdbTableFormJson = JObject.Parse(LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Json/InitDb-TableFormMapping.json"));
                var pageJson = JObject.Parse(LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Json/PageSchema.json"));
                var contentJson = JObject.Parse(LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Json/ContentSchema.json"));
                 
                var runner = ctx.Service<IMigrationRunner>();

                //var initialDbMigration = new InitialDbMigration(initdbJson);
                //var initialTableFormDbMigration = new InitialDbMigration(initdbTableFormJson);
                var pageMigration = new InitialDbMigration(pageJson);
                //var contentMigration = new ApplicatoinDbMigration(contentJson);
                runner.Up(pageMigration);
                //runner.Up(pageMigration);
                //runner.Up(contentMigration);
            });
        }
    }
}
