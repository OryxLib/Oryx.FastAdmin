using Oryx.FastAdmin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.BusinessModel
{
    public class ConfigEntry : BaseModel
    {
        //微信公众号设置
        public string AppId { get; set; }

        public string AppSecret { get; set; }
    }
}
