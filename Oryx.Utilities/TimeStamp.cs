using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities
{
    public class TimeStamp
    {
        public static long Get()
        {
            return (long)(DateTime.UtcNow - DateTime.Parse("1970-01-01 00:00:00")).TotalMilliseconds;///1000;
        }

        public static long Get(DateTime createTime)
        {
            return (long)(createTime.AddHours(-8) - DateTime.Parse("1970-01-01 00:00:00")).TotalMilliseconds;///1000;
        }
    }
}
