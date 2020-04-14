using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{
    public class ContentEntry : BaseModel
    {
        [ModelType(Name = "封面", ControlName = "img")]
        public string Cover { get; set; }

        [ModelType(Name = "标题", ControlName = "input")]
        public string Name { get; set; }

        [ModelType(Name = "作者", ControlName = "input")]
        public string Author { get; set; }

        [ModelType(Name = "关键字", ControlName = "input")]
        public string Keywords { get; set; }

        [ModelType(Name = "简介", ControlName = "input")]
        public string Descrtion { get; set; }

        [ModelType(Name = "内容", ControlName = "textarea", ShowOnList = false)]
        [SugarColumn(ColumnDataType = "MEDIUMTEXT")]
        public string Content { get; set; }

        [ModelType(Name = "访问数", ControlName = "number")]
        public int Views { get; set; }

        [ModelType(Name = "更新时间", ControlName = "input")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        [ModelType(Name = "分类", ControlName = "list"
            , DataSourceTable = "Category"
            , DataSourceTableValue = "Name"
            , ShowOnList = false)]
        [SugarColumn(IsNullable = true, Length = 255)]
        public Guid? CategoryId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Category Category { get; set; }

        [ModelType(Name = "系列"
            , ControlName = "list"
            , DataSourceTable = "GroupContentEntry"
            , DataSourceTableValue = "Name"
            , ShowOnList = false)]
        [SugarColumn(IsNullable = true, Length = 255)]
        public Guid? GroupContentId { get; set; }

        [ModelType(Name = "文件资源", ControlName = "filelist")]
        [SugarColumn(IsIgnore = true, IsNullable = true, Length = 255)]
        public string FileList { get; set; }

        [ModelType(Name = "系列名", ControlName = "input")]
        [SugarColumn(IsNullable = true, Length = 255)]
        public string GroupContentName { get; set; }

        [ModelType(ShowOnList = false)]
        [SugarColumn(IsIgnore = true)]
        public virtual List<Comments> Comments { get; set; }
    }
}
