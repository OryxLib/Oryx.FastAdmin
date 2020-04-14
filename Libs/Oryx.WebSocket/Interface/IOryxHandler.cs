using Oryx.WebSocket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.WebSocket.Interface
{
    public interface IOryxHandler
    {
        Task OnReciveMessage(OryxWebSocketMessage msg);
        Task OnClose(OryxWebSocketMessage msg);
    }
}
