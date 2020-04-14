using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.TemplateGenerator
{
    public class TemplateMetaData
    {
        public string Name { get; set; }
        public string EleType { get; internal set; }
        public object Data { get; internal set; }
        public List<string> Columns { get; set; }
        public object Link { get; internal set; }
    }
}
