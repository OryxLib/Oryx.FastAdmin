using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using Oryx.IoTHub.Library.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.IoTHub.Server
{
    public class MQTTServer
    {
        IMqttServer mqttServer;
        public MQTTServer()
        {
            var serverOptBuilder = new MqttServerOptionsBuilder();
            serverOptBuilder.WithDefaultEndpoint()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(108)
                .WithSubscriptionInterceptor(c =>
                {
                    c.AcceptSubscription = true;
                })
                .WithApplicationMessageInterceptor(c =>
                {
                    c.AcceptPublish = true;
                });

            var options = serverOptBuilder.Build();

            mqttServer = new MQTTnet.MqttFactory().CreateMqttServer();

            var recieveHander = new MsgRecieveHandler();
            var connectedHandler = new MqttServerClientConnectedHandler();
            var subsciptionHandler = new MqttServerClientSubscribedTopicHandler();
            mqttServer.ApplicationMessageReceivedHandler = recieveHander;
            mqttServer.ClientConnectedHandler = connectedHandler;
            mqttServer.ClientSubscribedTopicHandler = subsciptionHandler;

            mqttServer.StartAsync(options).Wait();

            var tc = new TimerCallback(timerCallback);

            var timer = new System.Threading.Timer(tc);
            timer.Change(3000, 3000);
        }

        void timerCallback(object state)
        {
            var appMsgBuilder = new MqttApplicationMessageBuilder();
            var msg = Encoding.UTF8.GetBytes("我是服务端");
            appMsgBuilder.WithPayload(msg)
                .WithTopic("家/客厅/空调/开关");

            var appMsg = appMsgBuilder.Build();
            mqttServer.PublishAsync(appMsg).Wait();
        }
    }
}
