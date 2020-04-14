using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.TemplateGenerator
{
    public class TemplateMetaDataAttribute : Attribute
    {
        public string Name { get; set; }
        public string Link { get; internal set; }

        public TemplateMetaDataAttribute()
        {

        }
    }
}
