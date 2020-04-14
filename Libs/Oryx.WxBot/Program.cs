using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Oryx.WxBot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TulingWXBot newbot = new TulingWXBot();
                //SimpleWXbot newbot = new SimpleWXbot();
                newbot.run();
            }
            catch (Exception ex)
            {
                Log.WriteDebug(ex.ToString());
            }
            Console.Read();
        }
    }
}
