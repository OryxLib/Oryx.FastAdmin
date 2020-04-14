using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core3.Models;
using Oryx.FastAdmin.Model.Contents;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class ContentController : FrontendBaseController
    {
        SqlSugarClient dbClient;
        public ContentController(SqlSugarClient _dbClient) : base(_dbClient)
        {
            dbClient = _dbClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            var contentEntry = await dbClient.Queryable<ContentEntry>().FirstAsync(x => x.Id == Id);

            ViewData["ContentEntry"] = contentEntry;
            contentEntry.Views += 1;
            await dbClient.Updateable(contentEntry).ExecuteCommandAsync();

            if (contentEntry.GroupContentId != null)
            {
                ViewData["GroupEntityList"] = await dbClient.Queryable<ContentEntry>().FirstAsync(x => x.GroupContentId == contentEntry.GroupContentId);
            }

            return View();
        }

        public async Task<IActionResult> GroupDetail(Guid Id)
        {
            ViewData["GroupContentEntry"] = await dbClient.Queryable<GroupContentEntry>().FirstAsync(x => x.Id == Id);

            var contentList = await dbClient.Queryable<ContentEntry>()
                .Where(x => x.GroupContentId == Id).Select(x => new ContentViewModel
                {
                    Name = x.Name,
                    Id = x.Id.ToString(),
                    Description = x.Descrtion,
                    Image = x.Cover
                }).ToListAsync();

            ViewData["ContenModels"] = contentList;

            return View();
        }
    }
}