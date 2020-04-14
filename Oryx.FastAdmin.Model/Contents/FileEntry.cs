using Oryx.FastAdmin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{
    public class FileEntry : BaseModel
    { 
        public string Icon { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public string ActualPath { get; set; }
         
        public int Order { get; set; }

        public string PhysicalPath { get; set; } 
         
        public string Extension { get; set; }
    }
}
