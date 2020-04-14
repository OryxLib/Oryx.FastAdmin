using Microsoft.AspNetCore.Routing;
using Oryx.Web.Core.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Web.Core.Routes
{
    public class RouteTableItem
    {
        public string Path { get; set; }

        public Func<OryxWebContext, Task> Func { get; set; }
        public string Method { get; internal set; }
        public RouteValueDictionary RouteValue { get; internal set; }
    } 
}
