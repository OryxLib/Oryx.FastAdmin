using FluentMigrator.Runner;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.Database;
using Oryx.Database.DbSchema.Mapper;
using Oryx.Database.Tools;
using Oryx.Web.Core.Actions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oryx.Web.Core.WebInstance
{
    public partial class DbSchemaModule : OryxWebModule
    {
        public DbSchemaModule()
        {
            SetStaticFiles("dbschema", @"OryxWeb\wwwroot\schema-designer\build");

            Get("/dbschema", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\schema-designer\build\index.html");
            });

            Post("/dbschema/saveData", async ctx =>
            {
                try
                {
                    var tool = new DataJsonDiff();
                    var srcjson = JObject.Parse(File.ReadAllText("database.json"));
                    var inTable = tool.InCommingTables(ctx.JsonObj, srcjson);
                    var outTable = tool.OutCommingTables(ctx.JsonObj, srcjson);

                    var inCol = tool.InCommingColumns(ctx.JsonObj, srcjson);
                    var outCol = tool.OutCommingColumn(ctx.JsonObj, srcjson);

                    var inRel = tool.InCommingRelation(ctx.JsonObj, srcjson);
                    var outRel = tool.OutCommingRelation(ctx.JsonObj, srcjson);

                    File.WriteAllText("database.json", ctx.JsonObj.ToString());
                    await ctx.Ajax(new
                    {
                        success = true
                    });
                }
                catch (Exception exc)
                {

                }

            });

            Post("/dbschema/updateDb", async ctx =>
            {
                try
                {
                    var tool = new DataJsonDiff();
                    if (!File.Exists("databaseProcessed.json"))
                    {
                        File.AppendAllText("databaseProcessed.json", JsonConvert.SerializeObject(new
                        {
                            tables = Array.Empty<object>(),
                            columns = new Dictionary<object, object>(),
                            relations = Array.Empty<object>()
                        }));
                    }
                    var targJsonStr = File.ReadAllText("database.json");
                    var tarJson = JObject.Parse(targJsonStr);
                    var srcjson = JObject.Parse(File.ReadAllText("databaseProcessed.json"));
                    var migtool = new ApplicatoinDbMigration(tarJson, srcjson);

                    var inTable = tool.InCommingTables(tarJson, srcjson);
                    var outTable = tool.OutCommingTables(tarJson, srcjson);

                    var inCol = tool.InCommingColumns(tarJson, srcjson);
                    var outCol = tool.OutCommingColumn(tarJson, srcjson);

                    var inRel = tool.InCommingRelation(tarJson, srcjson);
                    var outRel = tool.OutCommingRelation(tarJson, srcjson);

                    //add db
                    var db = ctx.Service<SqlSugarClient>();
                    //var tableSchema = db.Context.Ado.ExecuteCommand();
                    //var columnSchema = db.Insertable
                    //var referenceSchema = db.Queryable("DbReferenceSchema", "DbReferenceSchema");
                    foreach (var tableItem in inTable)
                    {
                        var tableDic = new Dictionary<string, object>();
                        tableDic.Add("Id", Guid.NewGuid());
                        tableDic.Add("Name", tableItem);
                        tableDic.Add("CreateTime", DateTime.Now);
                        db.Insertable(tableDic).AS("DbTableSchema").ExecuteReturnIdentity();
                    }

                    foreach (var colItem in inCol)
                    {
                        foreach (var item in colItem.Value)
                        {
                            var jobj = item as JObject;
                            var colDic = new Dictionary<string, object>();
                            colDic.Add("Id", Guid.NewGuid());
                            colDic.Add("Name", jobj.Value<string>("name"));
                            colDic.Add("Type", jobj.Value<string>("type"));
                            colDic.Add("Table", FindTableName(colItem.Key, tarJson["tables"]));
                            colDic.Add("CreateTime", DateTime.Now);
                            db.Insertable(colDic).AS("DbColumnSchema").ExecuteReturnIdentity();
                        }
                    }

                    foreach (var tableItem in inRel)
                    {
                        var refDic = new Dictionary<string, object>();
                        refDic.Add("Id", Guid.NewGuid());
                        refDic.Add("FromTable", tableItem.Name.Value);
                        refDic.Add("FromColumn", tableItem.Type.Value);
                        refDic.Add("ToTable", tableItem.Table.Value);
                        refDic.Add("TOColumn", DateTime.Now);
                        db.Insertable(refDic).AS("DbTableFormMapping").ExecuteReturnIdentity();
                    }

                    //migration.GetUpExpressions(ctx.Service<IMigrationContext>());
                    //var runner = ctx.Service<IMigrationRunner>();
                    //runner.Up(migtool); 
                    //File.WriteAllText("databaseProcessed.json", targJsonStr);
                    await ctx.Ajax(new
                    {
                        success = true
                    });
                }
                catch (Exception exc)
                {

                }

            });

            Get("/dbschema/loadData", async ctx =>
            {
                var json = File.ReadAllText("database.json");
                await ctx.Ajax(new { success = true, schema = json });
            });
            Get("/dbconfig", async ctx =>
            {
                //var dataTableList = new List<DataTable>();
                //dataTableList.Add(new DataTable
                //{
                //    DataTableItem = new List<DataTableItem> {
                //          new DataTableItem{
                //               Id = Guid.NewGuid(),
                //               Name="Test"
                //          }
                //     },
                //    Id = Guid.NewGuid(),
                //    Name = "TestTable",
                //    SoftDelete = true
                //});
                //var migration = new EmbeddedDbMigration(dataTableList);
                //migration.GetUpExpressions(ctx.Service<IMigrationContext>());
                //var runner = ctx.Service<IMigrationRunner>();
                //runner.Up(migration);
            });
            Get("/dbschema/saveData1", async ctx =>
            {
                //var beanApi = ctx.Service<BeanApi>();
                //var mapper = new DbMapper();
                //mapper.Load("dbschema.json");
                //await ctx.Ajax(new { });
            });
            Get("/dbschema/saveData3", async ctx =>
            {
                //var db = ctx.Service<DbContext>();

                //await db.Table("TestTable").Set("Id", Guid.NewGuid()).Set("Test", "2344").Insert();
                //var list = new List<dynamic> {
                //    new {
                //        Name="11",
                //        Age=2
                //    },new {
                //        Name="22",
                //        Age=3
                //    },
                //    new {
                //        Name="33",
                //        Age=4
                //    }
                //};
                //await db.Table("TestTable").InsertList(list, (d, m) =>
                // {
                //     m.Set("Id", Guid.NewGuid());
                //     m.Set("Test", d.Name);
                // });
                //await ctx.Ajax(new { Success = true });
            });

        }

        private string FindTableName(string key, dynamic tables)
        {
            var tableList = tables as IEnumerable<dynamic>;
            return tableList.FirstOrDefault(x => x.id.Value == key).name.Value;
        }
    }
}
