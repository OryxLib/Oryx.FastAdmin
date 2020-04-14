using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.Model;
using Oryx.FastAdmin.Core.ViewModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core.Controller
{
    public class BaseBackendController<T> : BaseController
        where T : BaseModel, new()
    {
        SqlSugarClient dbClient;
        ModelMapper modelMapper;
        public BaseBackendController(SqlSugarClient _dbClient)
            : base(_dbClient)
        {
            dbClient = _dbClient;
            modelMapper = new ModelMapper(dbClient);
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            ViewData["ModelTable"] = await modelMapper.ModelToTable<T>();

            return View();
        }

        public async Task<IActionResult> List(string query, int page = 1, int limit = 15)
        {
            ApiMessage apiMsg;
            var code = 0;
            var count = 0;
            object data = null;
            var msg = "";
            //            code: 0
            //count: 1000
            //data: [,…]
            //msg: ""
            try
            {
                var sqlQuery = dbClient.Queryable<T>();
                if (!string.IsNullOrEmpty(query))
                {
                    sqlQuery = sqlQuery.Where(query);
                }

                count = await sqlQuery.CountAsync();
                data = await sqlQuery.OrderBy(x => x.CreateTime, OrderByType.Desc).ToPageListAsync(page, limit);
            }
            catch (Exception exc)
            {
                msg = exc.Message;
            }

            return Json(new { code, count, data, msg });
        }

        public async Task<IActionResult> AddOrUpdate(Guid? Id)
        {
            ViewData["ModelType"] = await modelMapper.ModelToFormControl<T>();

            if (Id != null)
            {
                var data = await dbClient.Queryable<T>().FirstAsync(x => x.Id == Id.Value);
                ViewData["ModelData"] = await modelMapper.ModelToData(data);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(T userInfoModel)
        {
            ViewData["ModelType"] = modelMapper.ModelToFormControl<T>();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (Guid.Empty == userInfoModel.Id)
                {
                    userInfoModel.Id = Guid.NewGuid();
                    await dbClient.Insertable(userInfoModel).ExecuteCommandAsync();
                }
                else
                {
                    await dbClient.Updateable(userInfoModel).ExecuteCommandAsync();
                }
            });

            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await dbClient.Deleteable<T>(x => x.Id == Id).ExecuteCommandAsync();
            });

            return Json(apiMsg);
        }
    }
}
