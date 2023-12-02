using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPrivateTourRepository : ICrudRepository<PrivateTour>
    {
        Result<List<PrivateTour>> GetAllByTourist(long touristId);
        PrivateTour Start(PrivateTour privateTour);
        PrivateTour Next(PrivateTour privateTour);
        PrivateTour Finish(PrivateTour privateTour);
    }
}
