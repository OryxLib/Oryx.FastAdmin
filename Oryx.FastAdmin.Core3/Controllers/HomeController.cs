using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oryx.FastAdmin.Core3.Areas.Admin.Models;
using Oryx.FastAdmin.Core3.Models;
using Oryx.FastAdmin.Model.Contents;
using SqlSugar;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqlSugarClient sugarClient;
        public HomeController(ILogger<HomeController> logger, SqlSugarClient _sugarClient)
        {
            _logger = logger;
            sugarClient = _sugarClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Banners"] = await sugarClient.Queryable<Banners>().ToListAsync();
            var allCategories = await sugarClient.Queryable<Category>().ToListAsync();
            ViewData["CategoryList"] = allCategories;

            var cateDict = new Dictionary<Category, List<ContentViewModel>>();
            foreach (var cate in allCategories)
            {
                var contentList = await sugarClient.Queryable<ContentEntry>()
                    .Where(x => x.CategoryId == cate.Id && x.GroupContentId == null).ToListAsync();
                var contentViewModelList = new List<ContentViewModel>();
                foreach (var contentItem in contentList)
                {
                    contentViewModelList.Add(new ContentViewModel
                    {
                        Id = contentItem.Id.ToString(),
                        Name = contentItem.Name,
                        Description = contentItem.Descrtion,
                        Image = contentItem.Cover,
                        IsGroup = false,
                        Keys = contentItem.Keywords,
                        CreateTime = contentItem.CreateTime
                    });
                } 

                var groupContentList = await sugarClient.Queryable<GroupContentEntry>()
                 .Where(x => x.CategoryId == cate.Id).ToListAsync();
                foreach (var contentItem in groupContentList)
                {
                    contentViewModelList.Add(new ContentViewModel
                    {
                        Id = contentItem.Id.ToString(),
                        Name = contentItem.Name,
                        Description = contentItem.Description,
                        Image = contentItem.Cover,
                        IsGroup = true,
                        Keys = contentItem.Keywords,
                        CreateTime = contentItem.CreateTime
                    });
                }

                var tmpList = contentViewModelList.OrderByDescending(x => x.CreateTime).ToList();
                cateDict.Add(cate, tmpList);
            }
            ViewData["ContenModels"] = cateDict;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
