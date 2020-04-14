using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Oryx.WebSocket.Interface;

namespace Oryx.WebSocket.Infrastructure
{
    public class OryxWebSocketPool
    {
        public delegate void WebSocketAddHander(OryxWebSocketPoolItem wsItem);

        public event WebSocketAddHander OnWebSocketAdd;

        public delegate void WebSocketRemoveHander(OryxWebSocketPoolItem wsItem);

        public event WebSocketRemoveHander OnWebSocketRemove;

        public List<OryxWebSocketPoolItem> WebSocketList { get; set; } = new List<OryxWebSocketPoolItem>();

        public OryxWebSocketPoolItem this[Guid indexToken]
        {
            get
            {
                return WebSocketList.FirstOrDefault(x => x.Token == indexToken);
            }
        }

        public void Add(OryxWebSocketPoolItem wsItem)
        {
            WebSocketList.Add(wsItem);
            if (OnWebSocketAdd != null)
            {
                OnWebSocketAdd(wsItem);
            }
        }

        public void Remove(Guid wsToken)
        {
            var socketItem = WebSocketList.FirstOrDefault(x => x.Token == wsToken);
            WebSocketList.RemoveAll(x => x.Token == wsToken);
            if (OnWebSocketRemove != null)
            {
                OnWebSocketRemove(socketItem);
            }
        }

        public List<OryxWebSocketPoolItem> GetByQuery(string socialMsgWSClientKey, string routingKey)
        {
            return WebSocketList.Where(x => x.QueryString[socialMsgWSClientKey] == routingKey).ToList();
        }
    }
}
