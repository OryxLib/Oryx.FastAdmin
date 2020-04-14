using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Oryx.FastAdmin.Core.Controller;
using Oryx.FastAdmin.Model.Scratch;
using Oryx.Utilities;
using SqlSugar;

namespace Oryx.FastAdmin.Core3.Areas.Scratch.Controllers
{
    [Area("Scratch")]
    public class AssetController : Controller
    {

        [Route("/Scratch/Asset/{key}")]
        public async Task<IActionResult> AssetPost(string key)
        {
            var fileStream = HttpContext.Request.Body;
            var reader = await HttpContext.Request.BodyReader.ReadAsync();

            var streamReader = new StreamReader(fileStream);
            var stream = new MemoryStream();
            await fileStream.CopyToAsync(stream);
            await QiniuTool.UploadImage(stream, key);
            var jobj = JObject.Parse("{}");
            jobj.Add("content-name", key);
            jobj.Add("status", "ok");

            return Content(jobj.ToString(), "application/json");
        }

        //[Route("/Scratch/Asset/internalapi/asset/{Id}.svg/get/")]
        //public async Task<IActionResult> AssetTmp(string Id)
        //{
        //    var root = AppDomain.CurrentDomain.BaseDirectory + "asset/internalapi/";

        //    if (!Directory.Exists(root))
        //    {
        //        Directory.CreateDirectory(root);
        //    }
        //    var filePath = Path.Combine(root, Id + ".svg");
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        var fileStream = System.IO.File.Create(filePath);
        //        var wc = new HttpClient();
        //        var response = await wc.GetAsync($"https://assets.scratch.mit.edu/internalapi/asset/{Id}.svg/get/");
        //        await response.Content.CopyToAsync(fileStream);
        //        //await stream.CopyToAsync(fileStream);
        //        fileStream.Position = 0;
        //        return File(fileStream, "image/svg+xml");
        //    }
        //    else
        //    {
        //        var fileStream = System.IO.File.OpenRead(filePath);
        //        return File(fileStream, "image/svg+xml");
        //    }
        //}

        //[Route("/Scratch/Asset/internalapi/asset/{Id}.png/get/")]
        //public async Task<IActionResult> AssetTmp2(string Id)
        //{
        //    var root = AppDomain.CurrentDomain.BaseDirectory + "asset/internalapi/";

        //    if (!Directory.Exists(root))
        //    {
        //        Directory.CreateDirectory(root);
        //    }
        //    var filePath = Path.Combine(root, Id + ".png");
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        var fileStream = System.IO.File.Create(filePath);
        //        var wc = new HttpClient();
        //        var response = await wc.GetAsync($"https://assets.scratch.mit.edu/internalapi/asset/{Id}.png/get/");
        //        await response.Content.CopyToAsync(fileStream);
        //        //await stream.CopyToAsync(fileStream);
        //        fileStream.Position = 0;
        //        return File(fileStream, "image/png");
        //    }
        //    else
        //    {
        //        var fileStream = System.IO.File.OpenRead(filePath);
        //        return File(fileStream, "image/png");
        //    }
        //}

        //[Route("/Scratch/Asset/internalapi/asset/{Id}.wav/get/")]
        //public async Task<IActionResult> AssetTmp3(string Id)
        //{
        //    var root = AppDomain.CurrentDomain.BaseDirectory + "asset/internalapi/";

        //    if (!Directory.Exists(root))
        //    {
        //        Directory.CreateDirectory(root);
        //    }
        //    var filePath = Path.Combine(root, Id + ".wav");
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        var fileStream = System.IO.File.Create(filePath);
        //        var wc = new HttpClient();
        //        var response = await wc.GetAsync($"https://assets.scratch.mit.edu/internalapi/asset/{Id}.wav/get/");
        //        await response.Content.CopyToAsync(fileStream);
        //        //await stream.CopyToAsync(fileStream);
        //        fileStream.Position = 0;
        //        return File(fileStream, "audio/wav");
        //    }
        //    else
        //    {
        //        var fileStream = System.IO.File.OpenRead(filePath);
        //        return File(fileStream, "audio/wav");
        //    }
        //}



        [Route("/Scratch/Asset/internalapi/asset/{Id}.{format}/get/")]
        public async Task<IActionResult> AssetTmp4(string Id, string format)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory + "asset/internalapi/";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var filePath = Path.Combine(root, Id + "." + format);
            if (!System.IO.File.Exists(filePath))
            {
                var fileStream = System.IO.File.Create(filePath);
                var wc = new HttpClient();
                var response = await wc.GetAsync($"https://assets.scratch.mit.edu/internalapi/asset/{Id}.{format}/get/");
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.CopyToAsync(fileStream);
                    fileStream.Position = 0;
                    return File(fileStream, GetContentType(format));
                }
                else
                {
                    return Redirect($"//mioto.milbit.com/{Id}.{format}");
                }
            }
            else
            {
                var fileSbytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileSbytes, GetContentType(format));
            }
        }

        [Route("/scratch/editor/static/extension-assets/scratch3_music/{Id}.{format}")]
        public async Task<IActionResult> AssetTmp5(string Id, string format)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory + "asset/scratch3_music/";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var filePath = Path.Combine(root, Id + "." + format);
            if (!System.IO.File.Exists(filePath))
            {
                var fileStream = System.IO.File.Create(filePath);
                var wc = new HttpClient();

                var response = await wc.GetAsync($"https://assets.scratch.mit.edu/internalapi/asset/{Id}.{format}/get/");
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.CopyToAsync(fileStream);
                    fileStream.Position = 0;
                    fileStream.Close();
                    var resultStream = await response.Content.ReadAsStreamAsync();
                    return File(resultStream, GetContentType(format));
                }
                else
                {
                    return Redirect($"//mioto.milbit.com/{Id}.{format}");
                }
            }
            else
            {
                var fileStream = System.IO.File.OpenRead(filePath);
                return File(fileStream, GetContentType(format));
            }
        }

        private string GetContentType(string format)
        {
            switch (format)
            {
                case "mp3":
                    return "audio/mp3";
                case "wav":
                    return "audio/wav";
                case "png":
                    return "image/png";
                case "jpeg":
                    return "image/jpeg";
                case "jpg":
                    return "image/jpg";
                default:
                    return "application/octet-stream";
            }
        }



        [Route("/Scratch/Asset/internalapi/cdn/{Id}/get/")]
        public async Task<IActionResult> AssetCDN(string Id)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory + "asset/cdn/";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var filePath = Path.Combine(root, Id);
            var contentType = Id.Contains("png") ? "image/png" : "image/svg+xml";
            if (!System.IO.File.Exists(filePath))
            {
                var fileStream = System.IO.File.Create(filePath);
                var wc = new HttpClient();
                var response = await wc.GetAsync($"https://cdn.assets.scratch.mit.edu/internalapi/asset/{Id}/get/");
                await response.Content.CopyToAsync(fileStream);
                //await stream.CopyToAsync(fileStream);
                fileStream.Position = 0;
                return File(fileStream, contentType);
            }
            else
            {
                var fileStream = System.IO.File.OpenRead(filePath);
                return File(fileStream, contentType);
            }
        }
    }
}