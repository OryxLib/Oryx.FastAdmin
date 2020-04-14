using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Oryx.Utilities.Template;
using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.Web.Core
{
    public class OryxWebContext
    {
        public HttpContext HttpContext { get; }

        public WebSocket WebSocket { get; set; }

        public Dictionary<string, string> ParamDictionary { get; }

        public string Body { get; set; }

        public dynamic JsonObj { get; set; }

        public T Json<T>()
        {
            var setting = new JsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(Body, setting);
        }

        public async Task Send(string content)
        {
            var buffer = new byte[1024 * 4];
            var arrByte = new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));
            await WebSocket.SendAsync(arrByte, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public Stream Stream { get; set; }

        public OryxWebContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
            ParamDictionary = new Dictionary<string, string>();
            HttpContext.Request.Query.ToList().ForEach(item =>
            {
                ParamDictionary.Add(item.Key, item.Value);
            });
        }

        public async Task Write(string content)
        {
            await HttpContext.Response.WriteAsync(content);
        }
        public async Task Write(Stream stream)
        {
            HttpContext.Response.ContentType = "application/octet-stream";
            await stream.CopyToAsync(HttpContext.Response.Body);
        }

        public async Task Ajax(object jsonObj)
        {
            HttpContext.Response.ContentType = "application/json";
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var jsonStr = JsonConvert.SerializeObject(jsonObj, jsonSetting);
            await HttpContext.Response.WriteAsync(jsonStr);
        }

        public async Task Render(string tempPath, object dataObj)
        {
            var tmpStr = ReadTemplateStr(tempPath);

            TemplateContext ctx = new TemplateContext();
            ctx.TemplateLoader = new OryxTemplateLoader(@"\OryxWeb\Shared");

            var scriptObject = new ScriptObject();
            scriptObject.Import(dataObj);
            ctx.PushGlobal(scriptObject);

            var template = Template.Parse(tmpStr);
            var result = await template.RenderAsync(ctx);
            HttpContext.Response.ContentType = "text/html";
            await HttpContext.Response.WriteAsync(result);
        }

        public async Task Render(string tempPath)
        {
            var tmpStr = ReadTemplateStr(tempPath);
            TemplateContext ctx = new TemplateContext();
            ctx.TemplateLoader = new OryxTemplateLoader(@"OryxWeb\Shared");
             
            var baseDir = AppContext.BaseDirectory;
            var dir = baseDir + Path.GetDirectoryName(tempPath);
             
            //var ll = ctx.TemplateLoader.GetPath(ctx, callerSpan, "index.html");

            var template = Template.Parse(tmpStr, dir,
                lexerOptions: new LexerOptions()
                {
                    EnableIncludeImplicitString = true
                ,
                    Mode = ScriptMode.Default
                });

            var result = await template.RenderAsync(ctx);
            //var eva = Template.Evaluate(tmpStr, ctx); 

            HttpContext.Response.ContentType = "text/html";
            await HttpContext.Response.WriteAsync(result);
        }

        public async Task RenderWithLayout(string tmepPath, string LayoutPath)
        {
            TemplateContext ctxsub = new TemplateContext();
            //获得子页面
            var subTemplate = GetTemplate(tmepPath, ctxsub);
            var tempResult = subTemplate.RenderAsync();

            //将父页面作为参数传入Layout
            TemplateContext ctxLayout = new TemplateContext();
            var scriptObject = new ScriptObject();
            scriptObject.Import(new { renderbody = tempResult });
            ctxLayout.PushGlobal(scriptObject);
            var layoutTemplate = GetTemplate(LayoutPath, ctxLayout);
            var result = await layoutTemplate.RenderAsync(ctxLayout);

            HttpContext.Response.ContentType = "text/html";
            await HttpContext.Response.WriteAsync(result);
        }

        public Template GetTemplate(string tempPath, TemplateContext ctx)
        {
            var tmpStr = ReadTemplateStr(tempPath);
            ctx.TemplateLoader = new OryxTemplateLoader(@"OryxWeb\Shared");

            var callerSpan = new SourceSpan();
            var baseDir = AppContext.BaseDirectory;
            var dir = baseDir + Path.GetDirectoryName(tempPath);

            callerSpan.FileName = dir + "\\index.html";

            //var ll = ctx.TemplateLoader.GetPath(ctx, callerSpan, "index.html");

            var template = Template.Parse(tmpStr, dir,
                lexerOptions: new LexerOptions()
                {
                    EnableIncludeImplicitString = true
                ,
                    Mode = ScriptMode.Default
                });
            return template;
        }

        private string ReadTemplateStr(string path)
        {
            var absolutePath = MapPath(path);
            return File.ReadAllText(absolutePath);
        }

        private string MapPath(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.TrimStart('/').Replace("/", "\\"));
        }

        public void RouteValue(RouteValueDictionary routeValue)
        {
            if (routeValue != null)
            {
                routeValue.ToList().ForEach(item =>
                {
                    ParamDictionary.Add(item.Key, item.Value.ToString());
                });
            }
        }

        public string this[string key]
        {
            get
            {
                if (!ParamDictionary.ContainsKey(key))
                {
                    return string.Empty;
                }
                return ParamDictionary[key];
            }
        }

        public T Service<T>()
        {
            return HttpContext.RequestServices.GetService<T>();
        }
    }

    public class GenericaJson<T> { }
}
