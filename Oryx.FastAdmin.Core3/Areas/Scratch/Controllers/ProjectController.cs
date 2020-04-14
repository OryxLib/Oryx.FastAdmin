using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Oryx.Authentication.Business;
using Oryx.Authentication.Model;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Core.Model;
using Oryx.FastAdmin.Core.ViewModel;
using Oryx.FastAdmin.Filters;
using Oryx.FastAdmin.Filters.Attributes;
using Oryx.FastAdmin.Model.Scratch;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Scratch.Controllers
{
    [Area("Scratch")]
    [PageRoleAuthentication("user")]
    public class ProjectController : BaseApiController<Project>
    {
        SqlSugarClient dbClient;
        UserAuthBusiness userAuthBusiness;
        ModelMapper modelMapper;
        public ProjectController(SqlSugarClient _dbClient,
            UserAuthBusiness _userAuthBusiness) : base(_dbClient)
        {
            dbClient = _dbClient;
            userAuthBusiness = _userAuthBusiness;
            modelMapper = new ModelMapper(dbClient);
        }

        public async Task<IActionResult> Info(Guid Id)
        {
            var userId = HttpContext.Session.GetString(UserAuthBusiness.UserAuthFrontendKey);
            //var userInfo = await userAuthBusiness.GetUser(userId);
            var projectModel = await dbClient.Queryable<Project>().Where(x => x.Id == Id).FirstAsync();
            if (projectModel.OwerId != userId)
            {
                ViewData["hasOwner"] = false;
            }
            else
            {
                ViewData["hasOwner"] = true;
                ViewData["ModelType"] = await modelMapper.ModelToFormControl<Project>();
                var data = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == Id);
                ViewData["ModelData"] = await modelMapper.ModelToData(data);
            }
            return View();
        }

        [PageIgnore]
        public async Task<IActionResult> Preview(Guid Id)
        {
            return View();
        }

        [PageIgnore]
        [Route("/scratch/preview/{Id}")]
        public async Task<IActionResult> PreviewPage(Guid Id)
        {
            ViewData["Id"] = Id.ToString();
            var projectModel = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == Id);
            ViewData["ProjectData"] = projectModel;
            projectModel.Views += 1;
            await dbClient.Updateable(projectModel).ExecuteCommandAsync();
            return View();
        }

        public async Task<IActionResult> ChangeShare(Guid Id, bool isTrue)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var updateCol = new Dictionary<string, object>();
                updateCol.Add("id", Id);
                updateCol.Add("Shared", isTrue);
                await dbClient.Updateable(updateCol).AS("Project").WhereColumns("id").ExecuteCommandAsync();
            });
            return Json(apiMsg);
        }

        [HttpPost]
        public async Task<IActionResult> PostInfo(Project project)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await dbClient.Updateable(project).ExecuteCommandAsync();
            });
            return Json(apiMsg);
        }

        [Route("/Scratch/Editor/{Id?}")]
        public async Task<IActionResult> Index(Guid? Id)
        {
            var hasPlay = HttpContext.Request.Path.Value.Contains("play");
            if (!hasPlay)
            {
                var userIdString = HttpContext.Session.GetString(UserAuthBusiness.UserAuthFrontendKey);
                if (Id == null || Id.Value == Guid.Empty)
                {
                    var userId = Guid.Parse(userIdString);
                    var userInfo = await userAuthBusiness.GetUser(userId);
                    var _Id = Guid.NewGuid();
                    InitProject(_Id, userInfo);
                    return Redirect("/Scratch/Editor/" + _Id);
                }
            }

            return View();
        }

        [Route("/Scratch/Editor/With/{Id?}")]
        public async Task<IActionResult> IndexWith(Guid Id)
        {
            var userIdString = HttpContext.Session.GetString(UserAuthBusiness.UserAuthFrontendKey);

            var userId = Guid.Parse(userIdString);
            var userInfo = await userAuthBusiness.GetUser(userId);
            var _Id = Guid.NewGuid();
            await WidthProject(_Id, Id, userInfo);
            return Redirect("/Scratch/Editor/" + _Id);
        }


        public void InitProject(Guid Id, UserAccount userInfo)
        {
            var dataJson = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "wwwroot/projectData.json");

            dbClient.Insertable(new Project
            {
                Id = Id,
                CreateTime = DateTime.Now,
                Thumbnail = "//scratchasset.pagetechs.com/thumb/90759915-5baa-4d88-b56f-d8a08a54c2c0.jpg",
                ///Thumbnail = "//scratchasset.pagetechs.com/thumb/" + Id + ".jpg",
                OwerId = userInfo.Id.ToString(),
                Owner = userInfo.UserName,
                Views = 0,
                Data = dataJson
            }).ExecuteCommand();
        }

        public async Task WidthProject(Guid Id, Guid WithId, UserAccount userInfo)
        {
            var oldData = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == WithId);
            oldData.Id = Id;
            oldData.Shared = false;
            oldData.OwerId = userInfo.Id.ToString();
            oldData.Owner = userInfo.UserName;
            oldData.Views = 0;
            oldData.CreateTime = DateTime.Now;
            oldData.Thumbnail = "//scratchasset.pagetechs.com/thumb/" + WithId + ".jpg";
            await dbClient.Insertable(oldData).ExecuteCommandAsync();
        }

        [HttpPut]
        [Route("/Scratch/Project/{Id?}")]
        public async Task<IActionResult> ProjectPut(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var dataJsonStr = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                //var dataJson = JsonConvert.SerializeObject(dataJsonStr);

                var hasProject = await dbClient.Queryable<Project>().AnyAsync(x => x.Id == Id);
                if (!hasProject)
                {
                    var userIdString = HttpContext.Session.GetString(UserAuthBusiness.UserAuthFrontendKey);
                    var userId = Guid.Parse(userIdString);
                    var userInfo = await userAuthBusiness.GetUser(userId);
                    InitProject(Id.Value, userInfo);
                }
                else
                {
                    var projectInfo = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == Id);
                    projectInfo.Thumbnail = "//scratchasset.pagetechs.com/thumb/" + Id + ".jpg";
                    projectInfo.Data = dataJsonStr;
                    await dbClient.Updateable<Project>(projectInfo).ExecuteCommandAsync();
                }
            });

            return Json(apiMsg);
        }

        [HttpGet]
        [Route("/Scratch/Project/{Id?}")]
        [PageIgnore]
        public async Task<IActionResult> Project(Guid? Id)
        {
            var projectInfo = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == Id);

            return Content(projectInfo.Data, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await dbClient.Deleteable<Project>().Where(x => x.Id == Id).ExecuteCommandAsync();
            });
            return Json(apiMsg);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(Guid Id)
        //{
        //    var data = await dbClient.Queryable<Project>().FirstAsync(x => x.Id == Id);

        //    if (data != null)
        //    {
        //        return Json(data);
        //    }
        //    else
        //    {
        //        var result = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "wwwroot/projectData.json");
        //        return Content(result, "application/json");
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> Post(Project item)
        //{
        //    var data = await dbClient.Updateable<Project>(item).ExecuteCommandAsync();
        //    return Json(data);
        //}

    }
}