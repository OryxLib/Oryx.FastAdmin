using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.SpiderModel
{
    public class SpiderTaskModel : BaseModel
    {
        [ModelType(Name = "任务名称")]
        public string GroupName { get; set; }

        /// <summary>
        /// Values: json,html,xml
        /// </summary>
        [ModelType(Name = "数据类型", ControlName = "list")]
        [ModelBindData(Key = "json", Value = "json")]
        [ModelBindData(Key = "html", Value = "html")]
        [ModelBindData(Key = "xml", Value = "xml")]
        public string DataType { get; set; }

        [ModelType(Name = "访问网址")]
        public string Url { get; set; }

        [SugarColumn(IsIgnore = true)]
        [ModelType(Name = "爬虫映射选项",
            ControlName = "dynamicgroup"
            )]
        public List<SpiderTaskItem> SpiderTaskItemList { get; set; }
    }

    public class SpiderTaskItem : BaseModel
    {
        /// <summary>
        /// SpiderTaskModel.Id
        /// </summary>
        [ModelType(ControlName = "hide")]
        public Guid TaskId { get; set; }

        [ModelType(Name = "映射数据库")]
        public string MappedDb { get; set; }
        /// <summary>
        /// ValueType: single,multi
        /// </summary>
        [ModelType(Name = "单/多 数据", ControlName = "list")]
        [ModelBindData(Key = "单", Value = "single")]
        [ModelBindData(Key = "多", Value = "multi")]
        public string ValueType { get; set; }
        [ModelType(Name = "CSS索引器")]
        public string Expression { get; set; }
        [ModelType(Name = "映射数据列")]
        public string MappedDbColumn { get; set; }

        [ModelType(Name = "下级爬虫"
            , ControlName = "list"
            , DataSourceTable = "SpiderTaskModel"
            , DataSourceTableValue = "GroupName"
            , ShowOnList = false)]
        [SugarColumn(IsNullable = true)]
        public Guid NextTaskId { get; set; }
    }
}
