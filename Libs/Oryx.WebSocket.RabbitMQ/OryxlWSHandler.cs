using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Oryx.RabbitMQService;
using Oryx.WebSocket.Infrastructure;
using Oryx.WebSocket.Interface;

namespace Oryx.WebSocket.RabbitMQExtension
{
    public class SocialOryxlWSHandler : IOryxHandler
    {
        public RabbitMQClient rabbitMQClient;
        public SocialMsgManager socialMsgManager;

        public SocialOryxlWSHandler(
            RabbitMQClient _rabbitMQClient,
            SocialMsgManager _socialMsgManager
            )
        {
            rabbitMQClient = _rabbitMQClient;
            socialMsgManager = _socialMsgManager;
        }

        public async Task OnClose(OryxWebSocketMessage msg)
        {

        }

        public async Task OnReciveMessage(OryxWebSocketMessage msg)
        {
            await Task.Run(() =>
          {
              var userId = msg.Query[SocialMsgManager.SocialMsgWSClientKey];
              socialMsgManager.SendMssage(userId, msg.Message);
          });
        }
    }
}
