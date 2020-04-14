using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.Redis
{
    public class RedisDocument<T>
    {
        public string Key { get; set; }

        public T Value { get; set; }

        public DateTime SetTime { get; set; }

        public DateTime ExpireTime { get; set; }
        public bool IsExpired
        {
            get
            {
                return ExpireTime < DateTime.Now;
            }
        }
    }
}
