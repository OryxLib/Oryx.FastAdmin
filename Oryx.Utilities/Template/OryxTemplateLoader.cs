using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Utilities.Template
{
    public class OryxTemplateLoader : ITemplateLoader
    {
        private string RootPath;
        public OryxTemplateLoader(string rootPath)
        {
            RootPath = rootPath;
        }
        public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
        {
            return Path.Combine(AppContext.BaseDirectory + RootPath, templateName);
        }

        public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            return File.ReadAllText(templatePath);
        }

        public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            return await Task.Run(() => { return File.ReadAllText(templatePath); });
        }
    }
}
