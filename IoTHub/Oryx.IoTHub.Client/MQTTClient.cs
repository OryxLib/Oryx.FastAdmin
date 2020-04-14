using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using Oryx.IoTHub.Library.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Oryx.IoTHub.Client
{
    public class MQTTClient
    {
        public MQTTClient()
        {
            var mqttClient = new MqttFactory().CreateMqttClient();
      
            var optionBuilder = new MqttClientOptionsBuilder();
            optionBuilder
                .WithClientId("c001")
                .WithTcpServer(opts =>
                {
                    opts.Server = "127.0.0.1";
                    opts.Port = 108;
                })
                .WithCredentials("u001", "p001")
                .WithCleanSession(true);

            var options = optionBuilder.Build();
            mqttClient.ConnectAsync(options, CancellationToken.None).Wait();

            var recieveHander = new MsgRecieveHandler(); 
            mqttClient.ApplicationMessageReceivedHandler = recieveHander;
            

            var topicBuilder = new TopicFilterBuilder();
            topicBuilder.WithTopic("家/客厅/空调/#")
                .WithAtMostOnceQoS();
            var topicHome = topicBuilder.Build();

            var subscribOptionBuilder = new MqttClientSubscribeOptionsBuilder();
            subscribOptionBuilder.WithTopicFilter(topicHome);

            var subOpt = subscribOptionBuilder.Build();
            mqttClient.SubscribeAsync(subOpt, CancellationToken.None).Wait();

            var appMsg = new MqttApplicationMessage();
            
            appMsg.Topic = "家/客厅/空调/开关";
            appMsg.Payload = Encoding.UTF8.GetBytes("我来了~");
            appMsg.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            appMsg.Retain = false;


            mqttClient.PublishAsync(appMsg, CancellationToken.None).Wait();
            //var appMsg = new MqttApplicationMessage("家/客厅/空调/开关",
            //    Encoding.UTF8.GetBytes("消息内容"),
            //    MqttQualityOfServiceLevel.AtMostOnce, false);
        }
    }
}
