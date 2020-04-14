using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Oryx.FastAdmin.Core.Model
{ 
    public class BaseModel
    {
        [ModelType(ControlName = "hide")]
        [SugarColumn(IsPrimaryKey = true, Length = 255)]
        public Guid Id { get; set; }

        [ModelType(ControlName = "hide")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
