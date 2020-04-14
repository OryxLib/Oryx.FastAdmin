using Oryx.WebSocket.Infrastructure;
using Oryx.WebSocket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oryx.WebSocket.Extension.Utility;

namespace Oryx.FastAdmin.Core3.WebSocketHandler
{
    public class ChatHandler : IOryxHandler
    {
        public ChatHandler()
        {

        }
        public async Task OnClose(OryxWebSocketMessage msg)
        {

        }

        public async Task OnReciveMessage(OryxWebSocketMessage msg)
        {
            await msg.WebSocket.SendAsync(msg.Message);
        }
    }
}
