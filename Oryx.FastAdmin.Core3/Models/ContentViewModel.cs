using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.Models
{
    public class ContentViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Keys { get; set; }

        public string Image { get; set; }

        public bool IsGroup { get; set; }

        public string Id { get; set; }
        public DateTime CreateTime { get; internal set; }
    }
}
