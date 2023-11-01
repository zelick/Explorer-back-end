using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum RequestStatus
    {
        OnHold,
        Accepted,
        Rejected
    }
    public class ObjectRequest : Entity
    {
        public int MapObjectId { get; init; }
        public int AuthorId { get; init; }
        public RequestStatus Status { get; init; }
    }
}
