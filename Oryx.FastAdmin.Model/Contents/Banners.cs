using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{ 
    
    public class Banners : BaseModel
    {
        [ModelType(Name = "名称")]
        public string Name { get; set; }

        [ModelType(Name = "图片", ControlName = "img")]
        public string Img { get; set; }

        [ModelType(Name = "描述")]
        public string Description { get; set; }

        [ModelType(Name = "链接")]
        public string Url { get; set; }
    }
}
