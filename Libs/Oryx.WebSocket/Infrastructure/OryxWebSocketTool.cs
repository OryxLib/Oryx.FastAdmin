using Microsoft.AspNetCore.Http;
using Oryx.WebSocket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.WebSocket.Infrastructure
{
    public class OryxWebSocketTool
    {
        private OryxWebSocketPool websocketPool { get; set; }

        public OryxWebSocketTool(OryxWebSocketPool _websocketPool)
        {
            websocketPool = _websocketPool;
        }

        public async Task ProcessWebSocket(HttpContext context, KeyValuePair<string, IOryxHandler> currentOption)
        {
            var localWebsocketToken = Guid.NewGuid();
            try
            {
                var websocketInstance = await context.WebSockets.AcceptWebSocketAsync();
                var wsPoolItem = new OryxWebSocketPoolItem
                {
                    Token = localWebsocketToken,
                    Handler = currentOption.Value,
                    OryxWebSocket = websocketInstance,
                    Path = currentOption.Key,
                    QueryString = context.Request.Query.Keys.Cast<string>().ToDictionary(x => x, v => context.Request.Query[v][0])
                };
                websocketPool.Add(wsPoolItem);
                await ReciveMessage(wsPoolItem);
            }
            catch (Exception exc)
            {
                websocketPool.Remove(localWebsocketToken);
            }
        }

        public async Task ReciveMessage(OryxWebSocketPoolItem wsPoolItem)
        {
            while (wsPoolItem.OryxWebSocket.State == System.Net.WebSockets.WebSocketState.Open)
            {
                var bytes = new Byte[4096];
                var segment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                var reciver = await wsPoolItem.OryxWebSocket.ReceiveAsync(segment, CancellationToken.None);
                switch (reciver.MessageType)
                {
                    case System.Net.WebSockets.WebSocketMessageType.Binary:
                        break;
                    case System.Net.WebSockets.WebSocketMessageType.Close:
                        var closeMsg = new OryxWebSocketMessage()
                        {
                            Message = string.Empty,
                            WebSocket = null,
                            WSToken = wsPoolItem.Token,
                            Query = wsPoolItem.QueryString
                        };
                        websocketPool.Remove(wsPoolItem.Token);
                        await wsPoolItem.Handler.OnClose(closeMsg);
                        break;
                    case System.Net.WebSockets.WebSocketMessageType.Text:
                        var resultMsg = Encoding.UTF8.GetString(segment.Array);
                        var msg = new OryxWebSocketMessage()
                        {
                            Message = resultMsg.Trim('\0'),
                            WebSocket = wsPoolItem.OryxWebSocket,
                            WSToken = wsPoolItem.Token,
                            Query = wsPoolItem.QueryString
                        };
                        await wsPoolItem.Handler.OnReciveMessage(msg);
                        break;
                }
            }
        }
    }
}
