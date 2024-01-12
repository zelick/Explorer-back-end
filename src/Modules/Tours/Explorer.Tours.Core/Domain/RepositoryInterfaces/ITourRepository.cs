using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository : ICrudRepository<Tour>
    {
        bool Exists(long id);
        List<Tour> GetTours(List<long> tourIds);
        List<Tour> GetToursByAuthor(long id);
        List<Tour> GetToursByIds(List<long> tourIds);
        List<Tour> GetPublishedTours();
        Tour Close(long id);
        List<Tour> GetPublishedToursByAuthor(long authorId);
        List<Tour> GetTours();
    }
}
