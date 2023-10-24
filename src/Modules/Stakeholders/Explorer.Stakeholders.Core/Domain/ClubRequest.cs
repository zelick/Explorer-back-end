using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubRequest : Entity
    {
        public int ClubId { get; private set; }
        public int TouristId { get; private set; }
        //public ClubRequestStatus Status { get; init; }
        public String Status { get; init; }

        public ClubRequest(int clubId, int touristId, String status)
        {
            ClubId = clubId;
            TouristId = touristId;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            //if (string.IsNullOrWhiteSpace(Status)) throw new ArgumentException("Invalid Status");
            //if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            //if (ClubId == 0) throw new ArgumentException("Invalid ClubId");
           
        }
    }
}

/*public enum ClubRequestStatus
{
    Processing,
    Rejected,
    Accepted
}*/