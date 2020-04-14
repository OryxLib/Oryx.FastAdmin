using Newtonsoft.Json.Linq; 
using Oryx.Utilities.ValueType;
using Oryx.Web.Core.Actions;
using Oryx.Web.Core.WebInstance.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Web.Core.WebInstance
{
    public class APIModule : OryxWebModule
    {
        public APIModule()
        {
            Get("/db/schema", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = "DbColumnSchema";
                var data = await db.Queryable(tableName, tableName).Select("*").ToListAsync();
                await ctx.Ajax(data);
            });

            Get("/db/schema/{table}", async ctx =>
            {
                try
                {
                    var db = ctx.Service<SqlSugarClient>();
                    string tableName = "DbColumnSchema";
                    var table = db.Queryable(tableName, tableName);
                    var sugarParameter = new SugarParameter("name", ctx["table"]);
                    var param = new Dictionary<string, string>();
                    param.Add("name", ctx["table"]);
                    var data = await table.AS(tableName).Where($"{tableName}.Table=@name")
                    .AddParameters(param)
                    .Select("*")
                    .Take(100)
                    .ToListAsync();
                    await ctx.Ajax(new { success = true, data });
                }
                catch (Exception exc)
                {

                    throw;
                }
            });

            Post("/db/schema", async ctx =>
            {

            });

            Post("/db/all/{table}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                var query = ctx.Json<UniQueryParam>();
                if (!db.DbMaintenance.IsAnyTable(query.Table))
                {
                    await ctx.Ajax(new { success = false, message = "has no table" });
                }

                var sugarQuery = db.Queryable(query.Table, query.Table);
                sugarQuery = sugarQuery.AS(query.Table);
                sugarQuery = sugarQuery.Select("*");
                if (query.Where != null)
                {
                    foreach (var item in query.Where)
                    {
                        sugarQuery = sugarQuery.Where($"@p1{item[1]}@p2", new { p1 = item[0], p2 = item[2] });
                    }
                };

                if (query.Join != null)
                {

                }

                var data = await sugarQuery.ToListAsync();
                await ctx.Ajax(new { success = true, data });
            });

            Get("/db/{table}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = ctx["table"].ToString();
                if (db.DbMaintenance.IsAnyTable(tableName))
                {
                    var table = db.Queryable(tableName, tableName);
                    var data = table.AS(tableName).Select("*").Take(100).ToList();
                    await ctx.Ajax(new { success = true, data });
                }
                else
                {
                    await ctx.Ajax(new { success = false, message = "has no table" });
                }
            });

            Post("/db/insert/{table}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();

                string tableName = ctx["table"].ToString();
                if (db.DbMaintenance.IsAnyTable(tableName))
                {
                    if (ctx.Body.First() == '[')
                    {
                        var refDic = new Dictionary<string, object>();
                        var jsonArr = JArray.Parse(ctx.Body);
                        foreach (JObject json in jsonArr)
                        {
                            foreach (var item in json)
                            {
                                refDic.Add(item.Key, item.Value);
                            }
                            db.Insertable(refDic).AS(tableName).AddQueue();
                            await db.SaveQueuesAsync();
                        }
                    }
                    else
                    {
                        var refDic = new Dictionary<string, object>();
                        refDic.Add("Id", Guid.NewGuid().ToString());
                        var json = JObject.Parse(ctx.Body);
                        foreach (var item in json)
                        {
                            refDic.Add(item.Key, item.Value);
                        }
                        await db.Insertable(refDic).AS(tableName).ExecuteCommandAsync();
                    }

                    await ctx.Ajax(new { success = true });
                }
                else
                {
                    await ctx.Ajax(new { success = false, message = "has no table" });
                }
            });

            Post("/db/update/{table}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = ctx["table"].ToString();
                if (db.DbMaintenance.IsAnyTable(tableName))
                {
                    if (ctx.Body.First() == '[')
                    {
                        var refDic = new Dictionary<string, object>();
                        var jsonArr = JArray.Parse(ctx.Body);
                        foreach (JObject json in jsonArr)
                        {
                            foreach (var item in json)
                            {
                                refDic.Add(item.Key, item.Value);
                            }
                            db.Updateable(refDic).AS(tableName).AddQueue();
                            await db.SaveQueuesAsync();
                        }
                    }
                    else
                    {
                        var refDic = new Dictionary<string, object>();
                        var json = JObject.Parse(ctx.Body);
                        foreach (var item in json)
                        {
                            refDic.Add(item.Key, item.Value);
                        }
                        await db.Updateable(refDic).AS(tableName).ExecuteCommandAsync();
                        await ctx.Ajax(new { success = true });
                    }
                }
                else
                {
                    await ctx.Ajax(new { success = false, message = "has no table" });
                }
            });

            Post("/db/delete/{table}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = ctx["table"].ToString();
                if (db.DbMaintenance.IsAnyTable(tableName))
                {
                    //Todo 此处会有sql注入问题
                    db.Ado.ExecuteCommand($"delete from {tableName} where {ctx.JsonObj["where"]}");
                    await ctx.Ajax(new { success = true });
                }
                else
                {
                    await ctx.Ajax(new { success = false, message = "has no table" });
                }
            });


            //此接口可配置参数和查询;
            Get("/db/{table}/{pagesize}/{skipcount}", async ctx =>
            {
                var queryList = ctx.HttpContext.Request.Query;
                var db = ctx.Service<SqlSugarClient>();
                string tableName = ctx.JsonObj["table"].ToString();
                var data = db.Queryable(tableName, tableName).Select("*").Skip(ctx["skipcount"].Int()).Take(ctx["pagesize"].Int()).ToListAsync();
                await ctx.Ajax(data);
            });

            Post("/db/{table}/{pagesize}/{skipcount}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = ctx.JsonObj["table"].ToString();
                var query = db.Queryable(tableName, tableName).Select("*").Skip(ctx["skipcount"].Int()).Take(ctx["pagesize"].Int());

                //join table
                foreach (var table in ctx.JsonObj)
                {
                    var from = table.from;
                    var to = table.to;
                    var where = table.where;

                }
                var data = await query.ToListAsync();
                await ctx.Ajax(data);
            });

            Post("/db/insert/once/DbTableFormMapping", async ctx =>
            {

            });


            Post("/db/insert/", async ctx =>
            {
                var tableItem = ctx.JsonObj;
                //var set = ctx.JsonObj.set;
                var table = ctx.JsonObj.Table;
                var db = ctx.Service<SqlSugarClient>();

                var refDic = new Dictionary<string, object>();
                refDic.Add("Id", Guid.NewGuid());
                refDic.Add("Table", tableItem.Table.Value);
                refDic.Add("Form", tableItem.Form.Value);
                refDic.Add("FormConfig", tableItem.FormConfig.Value);
                refDic.Add("CreateTime", DateTime.Now);
                db.Insertable(refDic).AS("DbTableFormMapping").ExecuteReturnIdentity();

                await ctx.Ajax(new { success = true });
            });

            Post("/db/insert/once/{table}", async ctx =>
            {

            });


            Post("/db/insert/DbTableFormMapping", async ctx =>
            {
                var tableArr = ctx.JsonObj as List<dynamic>;
                //var set = ctx.JsonObj.set;
                // var table = ctx.JsonObj.Table;
                var db = ctx.Service<SqlSugarClient>();
                var listData = new List<Dictionary<string, object>>();
                tableArr.ForEach(tableItem =>
                {
                    var refDic = new Dictionary<string, object>();
                    refDic.Add("Id", Guid.NewGuid());
                    refDic.Add("Table", tableItem.Table.Value);
                    refDic.Add("Form", tableItem.Form.Value);
                    refDic.Add("FormConfig", tableItem.FormConfig.Value);
                    refDic.Add("CreateTime", DateTime.Now);
                    listData.Add(refDic);
                });
                db.Insertable(listData).AS("DbReferenceSchema").ExecuteReturnIdentity();

            });

            Post("/db/update", async ctx =>
            {
                var table = ctx.JsonObj.table;
                var where = ctx.JsonObj.where;
                var set = ctx.JsonObj.set;
            });

            Get("/schema", async ctx =>
            {
                //var db = ctx.Service<DbContext>();
                //DbTableQuery result = db.Table(ctx.Json["table"].ToString() + " t");
                //result = result.InnerJoin("DataTableItem d").On("t.Id=d.TableId");
                //var data = await result.Select("*").GetDataList();
                //await ctx.Ajax(data);
            });
            Post("/query2", async ctx =>
            {
                //var api = ctx.Service<BeanApi>();
                //var bean = api.Load(ctx.Json["table"].ToString());
                //await ctx.Ajax(bean);
            });
            Post("/query", async ctx =>
             {
                 var db = ctx.Service<SqlSugarClient>();
                 string tableName = ctx.JsonObj["table"].ToString();
                 var table = db.Queryable(tableName, tableName);
                 var data = table.AS(ctx.JsonObj["table"].ToString()).Select("*").ToList();
                 await ctx.Ajax(data);
             });

            Post("/insert", async ctx =>
            {
                //var db = ctx.Service<DbContext>();
                //DbTableQuery result = db.Table(ctx.Json["table"].ToString());
                //result = result
                //.Set("Id", Guid.NewGuid())
                //.Set("Test", ctx.Json["Test"]);
                //await result.Insert();
                //await ctx.Ajax(new { success = true });
            });

            Post("/delete/{table}/{id}", async ctx =>
            {
                try
                {
                    var db = ctx.Service<SqlSugarClient>();
                    await Task.Run(() => db.Ado.ExecuteCommand($"delete from {ctx["table"].Replace(" ", "")} where Id = @id",
                          //new SugarParameter("table", ctx["table"]),
                          new SugarParameter("id", ctx["id"])));
                    await ctx.Ajax(new { success = true });
                }
                catch (Exception exc)
                {
                    throw;
                }
            });
        }
    }
}
