using System;

namespace Oryx.IoTHub.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new MQTTServer();

            Console.WriteLine("SverStart !");
            Console.ReadLine();
        }
    }
}
