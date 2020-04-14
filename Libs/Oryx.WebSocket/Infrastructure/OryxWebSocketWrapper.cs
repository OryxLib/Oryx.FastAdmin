using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.WebSocket.Infrastructure
{
    public class OryxWebSocketWrapper
    {
        public Guid WebSocketToken { get; set; } = Guid.NewGuid();

        public System.Net.WebSockets.WebSocket WebSocketInstance { get; set; }

        public OryxWebSocketWrapper(System.Net.WebSockets.WebSocket websocket)
        {
            WebSocketInstance = websocket;
        }
    }
}
