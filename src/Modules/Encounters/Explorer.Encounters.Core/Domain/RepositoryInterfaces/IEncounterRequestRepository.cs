using Explorer.Encounters.Core.Domain.Encounters;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRequestRepository
    {
        EncounterRequest AcceptRequest(int id);
        EncounterRequest RejectRequest(int id);
    }
}
