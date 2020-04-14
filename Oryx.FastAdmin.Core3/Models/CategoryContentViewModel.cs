using Oryx.FastAdmin.Model.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core3.Models
{
    public class CategoryContentViewModel
    {
        public Category Category { get; set; }

        public List<ContentEntry> ContentEntries { get; set; }
    }
}
