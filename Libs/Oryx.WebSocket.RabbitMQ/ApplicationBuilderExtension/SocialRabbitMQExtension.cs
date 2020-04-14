using Microsoft.AspNetCore.Builder;
using Oryx.RabbitMQService;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Oryx.WebSocket.Infrastructure;
using Oryx.WebSocket.Extension.Utility;
using System.Threading;
using RabbitMQ.Client;

namespace Oryx.WebSocket.RabbitMQExtension.ApplicationBuilderExtension
{
    /// <summary>
    /// 注册社交功能到RabbitMQ
    /// </summary>
    public static class SocialRabbitMQExtension
    {
        public static OryxWebSocketPool wsPool;

        public static IApplicationBuilder UserSocialRabbitMQ(this IApplicationBuilder app)
        {
            var rmqClient = app.ApplicationServices.GetService<RabbitMQClient>();
            wsPool = app.ApplicationServices.GetService<OryxWebSocketPool>();
            rmqClient.RegisterReciverQueue(SocialMsgManager.SocialMsgWSRabbitMQKey, SocialMsgManager.SocialMsgWSRabbitMQExchange);
            rmqClient.RegisterQueueConsumer(SocialMsgManager.SocialMsgWSRabbitMQKey);
            rmqClient[SocialMsgManager.SocialMsgWSRabbitMQKey].Consumer.Received += Consumer_Received;
            return app;
        }

        /// <summary>
        /// 注册消息接收处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Consumer_Received(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
        {
            //var wsToken = Guid.Parse(e.RoutingKey);
            var wsLsit = wsPool.GetByQuery(SocialMsgManager.SocialMsgWSClientKey, e.RoutingKey);
            var arrByte = new ArraySegment<byte>(e.Body);
            wsLsit.ForEach(wsItem =>
            {
                wsItem.OryxWebSocket.SendAsync(arrByte, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);
            });
        }
    }
}
