using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Oryx.WebSocket.Infrastructure
{
    public class OryxWebSocketMessage
    {
        public string Message { get; internal set; }
        public System.Net.WebSockets.WebSocket WebSocket { get; internal set; }
        public Guid WSToken { get; internal set; }
        public Dictionary<string, string> Query { get; set; }
    }
}
