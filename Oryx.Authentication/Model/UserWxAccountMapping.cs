using Oryx.FastAdmin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Authentication.Model
{
   public class UserWxAccountMapping: BaseModel
    {
        public string UserId { get; set; }

        public string WxOpenId { get; set; }

        public string AppId { get; set; }
    }
}
