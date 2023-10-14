using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubRequest : Entity
    {
        public int ClubId { get; private set; }
        public int TouristId { get; private set; }
        public ClubRequestStatus Status { get; private set; } 

        public ClubRequest(int clubId, int touristId, ClubRequestStatus status)
        {
            ClubId = clubId;
            TouristId = touristId;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            
        }
    }

    public enum ClubRequestStatus
    {
        Processing,
        Rejected,
        Accepted
    }
}
