using Newtonsoft.Json.Linq;
using Oryx.Web.Core.Actions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance
{
    public class IntanceFDModule : OryxWebModule
    {
        public IntanceFDModule()
        {
            SetStaticFiles("frontenddesign", @"OryxWeb\wwwroot\frontendBuilder");
             
            Get("/fd", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\frontendBuilder\index.html");
            });


            Get("/page/editor/{id}", async ctx =>
            {
                await ctx.Render(@"OryxWeb\wwwroot\frontendBuilder\index2.html", new
                {
                    id = ctx.ParamDictionary["id"]
                });
            });

            Post("/page/store/{id}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = "Page";
                var count = await db.Queryable(tableName, tableName).Where("Id=@id", new { id = ctx.ParamDictionary["id"] }).CountAsync();
                if (count < 1)
                {
                    var refDic = new Dictionary<string, object>();
                    refDic.Add("Id", ctx.ParamDictionary["id"]);
                    var json = JObject.Parse(ctx.Body);
                    refDic.Add("Html", json["gjs-html"].ToString());
                    refDic.Add("Css", json["gjs-css"].ToString());
                    refDic.Add("Asset", json["gjs-assets"].ToString());
                    refDic.Add("Components", json["gjs-components"].ToString());
                    //foreach (var item in json)
                    //{
                    //    refDic.Add(item.Key, item.Value);
                    //}
                    await db.Insertable(refDic).AS(tableName).ExecuteCommandAsync();
                }
                else
                {
                    var refDic = new Dictionary<string, object>();
                    var json = JObject.Parse(ctx.Body);
                    refDic.Add("Id", ctx.ParamDictionary["id"]);
                    refDic.Add("Html", json["gjs-html"].ToString());
                    refDic.Add("Css", json["gjs-css"].ToString());
                    refDic.Add("Asset", json["gjs-assets"].ToString());
                    refDic.Add("Components", json["gjs-components"].ToString());
                    await db.Updateable(refDic).AS(tableName).Where("Id=@id").ExecuteCommandAsync();
                    await ctx.Ajax(new { success = true });
                }
            });

            Get("/page/load/{id}", async ctx =>
            {
                var db = ctx.Service<SqlSugarClient>();
                string tableName = "Page";
                var data = await db.Queryable(tableName, tableName)
                .Where("Id=@id", new { id = ctx.ParamDictionary["id"] }).ToDataTableAsync();
                var dataRow = data.Rows[0];
                var outDic = new Dictionary<string, string>();
                outDic.Add("gjs-html", dataRow["Html"].ToString());
                outDic.Add("gjs-css", dataRow["Css"].ToString());
                outDic.Add("gjs-assets", dataRow["Asset"].ToString());
                outDic.Add("gjs-components", dataRow["Components"].ToString());
                await ctx.Ajax(outDic);
            });
        }
    }
}
