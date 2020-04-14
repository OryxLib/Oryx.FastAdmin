using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.IoTHub.Library.Handlers
{
    public class MqttServerClientSubscribedTopicHandler : IMqttServerClientSubscribedTopicHandler
    {
        public async Task HandleClientSubscribedTopicAsync(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.ClientId + ":" + eventArgs.TopicFilter);
        }
    }
}
