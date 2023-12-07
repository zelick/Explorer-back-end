using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public enum RequestStatus
    {
        OnHold,
        Accepted,
        Rejected
    }
    public class EncounterRequest : Entity
    {
        public long EncounterId { get; init; }
        public int TouristId { get; init; }
        public RequestStatus Status { get; private set; }

        public EncounterRequest() { }

        public EncounterRequest(long encounterId, RequestStatus requestStatus, int touristId)
        {
            EncounterId = encounterId;
            Status = requestStatus;
            TouristId = touristId;
        }

        public void AcceptRequest()
        {
            Status = RequestStatus.Accepted;
        }

        public void RejectRequest()
        {
            Status = RequestStatus.Rejected;
        }
    }
}
