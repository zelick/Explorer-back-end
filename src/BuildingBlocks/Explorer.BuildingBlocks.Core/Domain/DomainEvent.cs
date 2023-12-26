using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        protected DomainEvent() { }
    }
}
