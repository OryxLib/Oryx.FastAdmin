using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.IoTHub.Library.Handlers
{
    public class MsgRecieveHandler : IMqttApplicationMessageReceivedHandler
    {
        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var msgString = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
            Console.WriteLine(msgString);
        }
    }
}
