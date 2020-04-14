using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{
    public class GroupContentEntry : BaseModel
    {
        [ModelType(Name = "标题", ControlName = "input")]
        public string Name { get; set; }

        [ModelType(Name = "关键词", ControlName = "input")]
        public string Keywords { get; set; }

        [ModelType(Name = "简介", ControlName = "textarea")]
        public string Description { get; set; }

        [ModelType(Name = "作者", ControlName = "input")]
        public string Author { get; set; }

        [ModelType(Name = "封面", ControlName = "img")]
        public string Cover { get; set; }

        [SugarColumn(IsNullable = false, Length = 255)]
        [ModelType(Name = "分类", ControlName = "list", DataSourceTable = "Category", DataSourceTableValue = "Name")]
        public Guid? CategoryId { get; set; }

        [ModelType(Name = "访问数", ControlName = "number")]
        public int Views { get; set; }
    }
}
