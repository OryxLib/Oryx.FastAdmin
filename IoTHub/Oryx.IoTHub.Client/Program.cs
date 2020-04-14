using System;

namespace Oryx.IoTHub.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MQTTClient();
            Console.WriteLine("Client Start!");
            Console.ReadLine();
        }
    }
}
