using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class CheckpointRequest : Entity
    {
        public int CheckpointId { get; init; }
        public int AuthorId { get; init; }
        public RequestStatus Status { get; private set; }

        public CheckpointRequest(int checkpointId, int authorId, RequestStatus status)
        {
            CheckpointId = checkpointId;
            AuthorId = authorId;
            Status = status;
        }

        public void RejectRequest()
        {
            Status = RequestStatus.Rejected;
        }

        public void AcceptRequest()
        {
            Status = RequestStatus.Accepted;
        }
    }
}
