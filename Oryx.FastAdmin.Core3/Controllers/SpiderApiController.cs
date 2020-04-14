using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.Spider;
using Oryx.FastAdmin.Core.SpiderModel;
using Oryx.FastAdmin.Core.ViewModel;
using SqlSugar;
using Oryx.FastAdmin.Core.Spider.Ultilities;
using OpenQA.Selenium;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class SpiderApiController : Controller
    {
        SqlSugarClient dbClient;
        public SpiderApiController(SqlSugarClient _dbClient)
        {
            dbClient = _dbClient;
        }

        [HttpPost]
        public async Task<IActionResult> GetResult(SpiderViewModel spiderViewModel)
        {
            var runner = new PhantomJsRunner(null, "", "");
            runner.WebDriver.Navigate().GoToUrl(spiderViewModel.Url);
            var result = runner.WebDriver.PageSource;

            var dataType = "";
            if (result.First() == '<')
            {
                if (result.Contains("<html>"))
                {
                    dataType = "html";
                }
                else
                {
                    dataType = "xml";
                }
            }
            else
            {
                dataType = "json";
            }

            return View();
        }

        public IActionResult CreateTask(SpiderTaskModel spiderTaskModels)
        {
            try
            {
                RecurringJob.AddOrUpdate(spiderTaskModels.Id.ToString(), () => ProcessSpiderTask(spiderTaskModels.Id), Cron.Daily);
            }
            catch (Exception exc)
            {

            }
            return Json(new { success = true });
        }

        public void ProcessSpiderTask(Guid Id)
        {
            var spiderTaskModel = dbClient.Queryable<SpiderTaskModel>().First(x => x.Id == Id);
            var spiderTaskItemList = dbClient.Queryable<SpiderTaskItem>().Where(x => x.TaskId == Id).ToList();
            var runner = new PhantomJsRunner(null, "", "");
            runner.WebDriver.Navigate().GoToUrl(spiderTaskModel.Url);

            switch (spiderTaskModel.DataType)
            {
                case "josn":
                    break;
                case "html":
                    var htmlElement = runner.WebDriver.FindElement(By.TagName("html"));
                    var dbDict = new Dictionary<string, object>();
                    foreach (var item in spiderTaskItemList)
                    {
                        if (item.ValueType == "single")
                        {
                            var targetValue = htmlElement.FindValueByCss(item.Expression);
                            dbDict.Add(item.MappedDbColumn, targetValue);
                        }
                        else
                        {
                            var targetValues = htmlElement.FindMultiValueByCss(item.Expression);
                            dbDict.Add(item.MappedDbColumn, string.Join(',', targetValues));
                        }
                    }
                    dbClient.Insertable(dbDict).AS(spiderTaskItemList.First().MappedDb).ExecuteCommandAsync();
                    break;
                case "xml":
                    break;
            }
        }
    }
}