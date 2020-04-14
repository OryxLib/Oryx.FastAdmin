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
    public class BaseApiController<T> : BaseController
             where T : BaseModel, new()
    {
        SqlSugarClient dbClient;
        ModelMapper modelMapper;
        public BaseApiController(SqlSugarClient _dbClient)
            : base(_dbClient)
        {
            dbClient = _dbClient;
            modelMapper = new ModelMapper(dbClient);
        }

        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await modelMapper.ModelToTable<T>();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> List(string query, int page = 1, int limit = 15)
        {
            ApiMessage apiMsg;
            var code = 0;
            var count = 0;
            object data = null;
            var msg = "";
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
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (Id != null)
                {
                    var data = await dbClient.Queryable<T>().FirstAsync(x => x.Id == Id.Value);
                    return await modelMapper.ModelToData(data);
                }
                return null;
            });
            return Json(apiMsg);
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
