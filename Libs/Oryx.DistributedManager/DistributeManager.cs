using System;

namespace Oryx.DistributedManager
{
    public class DistributeManager
    {
        public readonly Guid DistributeSystemID;

        public DistributeManager()
        {
            DistributeSystemID = Guid.NewGuid();
        }
    }
}
