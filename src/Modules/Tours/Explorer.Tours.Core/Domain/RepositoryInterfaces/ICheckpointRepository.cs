using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository : ICrudRepository<Checkpoint>
    {
        PagedResult<Checkpoint> GetPagedByTour(int page, int pageSize, int id);
    }
}
