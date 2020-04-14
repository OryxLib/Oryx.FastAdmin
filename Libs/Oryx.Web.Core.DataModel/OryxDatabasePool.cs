using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Oryx.Database
{
    public class OryxDatabasePool<T>
        where T : class
    {
        private readonly ConcurrentQueue<T> _pool = new ConcurrentQueue<T>();
        private int _maxSize;
        private int _count;
        private Func<T> _activator;
        public OryxDatabasePool(Func<T> func)
        {
            _activator = func;
        }

        public sealed class Lease : IDisposable
        {
            private OryxDatabasePool<T> _contextPool;
            public Lease(OryxDatabasePool<T> contextPool)
            {
                _contextPool = contextPool;
                Context = _contextPool.Rent();
            }

            public T Context { get; private set; }

            void IDisposable.Dispose()
            {
                if (_contextPool != null)
                {
                    if (!_contextPool.Return(Context))
                    {
                        //((IDbContextPoolable)Context).SetPool(null);
                        //Context.Dispose();
                    }

                    _contextPool = null;
                    Context = null;
                }
            }
        }

        public T Rent()
        {
            if (_pool.TryDequeue(out var context))
            {
                Interlocked.Decrement(ref _count);

                return context;
            }
            context = _activator();
            _pool.Enqueue(context);
            return context;
        }

        public bool Return(T context)
        {
            _pool.Enqueue(context);
            return true;
        }
    }
}
