using Scriban;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Web.Core.Templates
{
    public class HtmlTemplate
    {
        public static async Task<string> GenericTemplate()
        {
            try
            {
                //Template.Parse
                // Parse a scriban template
                var template = Template.Parse("Hello {{name}}!");
                var result = template.Render(new { Name = "World" }); // => "Hello World!" 
                return result;
            }
            catch (Exception exc)
            {

                throw;
            }

        }

        public static async Task<string> GenericTemplateWithObject()
        {
            try
            {
                //Template.Parse
                // Parse a scriban template
                var template = Template.Parse("Hello {{name}}!, {{user.test}}");
                var dataModel = new
                {
                    Name = "World",
                    User = new
                    {
                        Test = "111"
                    }
                };
                var result = await template.RenderAsync(dataModel); // => "Hello World!" 

                return result;
            }
            catch (Exception exc)
            {

                throw;
            }

        }

        public static async Task<string> GenericTemplateWithDictionary()
        {
            try
            {
                //Template.Parse
                // Parse a scriban template
                var template = Template.Parse("Hello {{test2.name}}!, {{test2.user.test}} , {{test1.name}}");
                var dataModel = new
                {
                    Name = "World-11",
                    User = new
                    {
                        Test = "111"
                    }
                };

                var dataModel2 = new
                {
                    Name = "World-22",
                    User = new
                    {
                        Test = "2222"
                    }
                };
                var dict = new Dictionary<string, object>();
                dict.Add("test1", dataModel);
                dict.Add("test2", dataModel2);
                var result = await template.RenderAsync(dict); // => "Hello World!" 

                return result;
            }
            catch (Exception exc)
            {

                throw;
            }

        }
    }
}
