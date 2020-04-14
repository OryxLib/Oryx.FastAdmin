using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oryx.Authentication.Business;
using Oryx.FastAdmin.Filters;
using Oryx.FastAdmin.Model.Scratch;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Scratch.Controllers
{
    [Area("Scratch")]

    public class HomeController : Controller
    {
        public SqlSugarClient sqlSugarClient;
        public HomeController(SqlSugarClient _sqlSugarClient)
        {
            sqlSugarClient = _sqlSugarClient;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString(UserAuthBusiness.UserAuthFrontendKey);

            ViewData["ProjectModelPublic"] = await sqlSugarClient.Queryable<Project>()
                .OrderBy(x => x.CreateTime, OrderByType.Desc)
                .Where(x => x.Shared)
                .ToListAsync();

            if (!string.IsNullOrEmpty(userId))
            {
                var userGuid = Guid.Parse(userId);
                ViewData["ProjectModel"] = await sqlSugarClient.Queryable<Project>()
                    .OrderBy(x => x.CreateTime, OrderByType.Desc)
                    .Where(x => x.OwerId == userId)
                    //.Take(4)
                    .ToListAsync();
            }

            return View();
        }


        //[Route("/Scratch/{Id?}")]
        //public IActionResult Index(Guid? Id)
        //{
        //    if (Id == null || Id.Value == Guid.Empty)
        //    {
        //        return Redirect("/Scratch/" + Guid.NewGuid());
        //    }
        //    return View();
        //}


        //[HttpPut]
        //[Route("/Scratch/Project/{Id?}")]
        //public async Task<IActionResult> ProjectPut(Guid? Id)
        //{
        //    var dataJsonStr = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    //var dataJson = JsonConvert.SerializeObject(dataJsonStr);
        //    await sqlSugarClient.Insertable<Project>(new Project
        //    {
        //        Id = Guid.NewGuid(),
        //        CreateTime = DateTime.Now,
        //        Data = dataJsonStr,
        //        Owner = "admin"
        //    }).ExecuteCommandAsync();

        //    return Json(new { });
        //}

        //[HttpGet]
        //[Route("/Scratch/Project/{Id?}")]
        //public IActionResult Project(Guid? Id)
        //{
        //    var result = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "wwwroot/projectData.json");
        //    return Content(result, "application/json");
        //    return Json(new { });
        //}
    }
}