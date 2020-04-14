using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Database
{
    public class DbOptions<T>
    {
        public string Name { get; set; }

        public T Instance { get; set; }

        public int PoolSize { get; set; } = 256;
    }
}
