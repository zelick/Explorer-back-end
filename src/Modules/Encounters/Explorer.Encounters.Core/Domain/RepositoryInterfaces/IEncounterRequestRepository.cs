using Explorer.Encounters.Core.Domain.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRequestRepository
    {
        EncounterRequest AcceptRequest(int id);
        EncounterRequest RejectRequest(int id);
    }
}
