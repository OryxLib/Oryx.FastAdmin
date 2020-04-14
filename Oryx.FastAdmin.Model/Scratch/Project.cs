using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Scratch
{
    public class Project : BaseModel
    {
        [SugarColumn(IsNullable = true, Length = 255)]
        public string Name { get; set; } = "未命名";

        public string Owner { get; set; }

        [SugarColumn(Length = 255)]
        public string OwerId { get; set; }

        public string Thumbnail { get; set; }

        [SugarColumn(ColumnDataType = "MEDIUMTEXT")]
        public string Data { get; set; }

        [ModelType(ControlName = "textarea")]
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true)]
        public string Description { get; set; }

        public bool Shared { get; set; }

        public int Views { get; set; }
    }
}
