using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.WebSocket.Extension.Utility
{
    public static class WebSocketExtension
    {
        static object lockObj = new object();
        public static async Task SendAsync(this System.Net.WebSockets.WebSocket webSocket, string msg)
        {
            try
            {
                await Task.Run(() =>
                {
                    var buffer = Encoding.UTF8.GetBytes(msg);
                    var arrSegment = new ArraySegment<byte>(buffer, 0, buffer.Length);
                    lock (lockObj)
                    {
                        webSocket.SendAsync(arrSegment, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                });
            }
            catch (Exception exc)
            {
                Console.WriteLine("wx 执行的错误");
                Console.WriteLine(exc.Message);
            }
        }
    }
}
