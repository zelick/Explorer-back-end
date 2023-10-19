using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository : CrudDatabaseRepository<Tour, ToursContext>, ITourRepository
    {
        //private readonly ToursContext _dbContext;

        public TourDatabaseRepository(ToursContext toursContext /*, ToursContext dbContext*/) : base(toursContext)
        {
            //_dbContext = dbContext;
        }

        public bool Exists(int id)
        {
            return DbContext.Tours.Any(t => t.Id == id);
        }

        public List<Tour> GetToursByAuthor(int id)
        {
            return DbContext.Tours
                .Include(t => t.Equipment)
                .Where(t => t.AuthorId == id)
                .ToList();
        }
    }
}
