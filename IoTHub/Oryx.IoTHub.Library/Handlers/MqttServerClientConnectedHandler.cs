using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.IoTHub.Library.Handlers
{
    public class MqttServerClientConnectedHandler : IMqttServerClientConnectedHandler
    {
        public async Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.ClientId);
        }
    }

}
