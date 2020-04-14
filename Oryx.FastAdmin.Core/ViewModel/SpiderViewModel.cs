using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.ViewModel
{
    public class SpiderViewModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
