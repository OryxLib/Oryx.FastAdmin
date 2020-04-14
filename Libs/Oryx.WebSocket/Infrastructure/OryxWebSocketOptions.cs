using Oryx.WebSocket.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.WebSocket.Infrastructure
{
    public class OryxWebSocketOptions
    {
        public Dictionary<string, IOryxHandler> OptionsDic { get; set; } = new Dictionary<string, IOryxHandler>();

        public void Register(string path, IOryxHandler handler)
        {
            OptionsDic.Add(path, handler);
        }
    }
}
