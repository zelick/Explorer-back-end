using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRequestRepository
    {
        CheckpointRequest AcceptRequest(int id);
        CheckpointRequest RejectRequest(int id);
    }
}
