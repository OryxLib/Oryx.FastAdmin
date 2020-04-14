using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{
    public class Category : BaseModel
    {
        [ModelType(Name = "分类标题")]
        public string Name { get; set; }

        [ModelType(Name = "关键词", ControlName = "input")]
        public string Keywords { get; set; }

        [ModelType(Name = "简介", ControlName = "input")]
        public string Description { get; set; }

        [ModelType(Name = "排名", ControlName = "number")]
        public int Order { get; set; }

        [SugarColumn(IsIgnore = true)]
        [ModelType(ControlName = "hide")]
        public virtual List<ContentEntry> ContentEntries { get; set; }

        [ModelType(Name = "访问数", ControlName = "number")]
        public int Views { get; set; }
    }
}
