using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Model.Contents
{
    public class Comments : BaseModel
    {
        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string TargetTable { get; set; }

        public string TargetId { get; set; }

        //public Guid ContentEntryId { get; set; }

        //[SugarColumn(IsIgnore = true)]
        //public ContentEntry ContentEntry { get; set; }

        //public Guid CategoryId { get; set; }

        //[SugarColumn(IsIgnore = true)]
        //public Category Category { get; set; }

    }
}
