using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Web.Core.WebInstance.Infrastructure
{
    public class UniQueryParam
    {
        [JsonProperty("table")]
        public string Table { get; internal set; }
        [JsonProperty("join")]
        public List<JoinTable> Join { get; internal set; }
        [JsonProperty("where")]
        public List<List<string>> Where { get; internal set; }
    }

    public class JoinTable
    {
        public string Table { get; set; }
    }
}
