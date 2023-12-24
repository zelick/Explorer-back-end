using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public abstract class DomainEvent
    {
        public long AggregateId { get; private set; }
        public DomainEvent(long parentId)
        {
            AggregateId = parentId;
        }
        public DomainEvent(int parentId)
        {
            AggregateId = parentId;
        }
    }
}
