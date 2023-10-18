using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository : ITourRepository
    {
        private readonly ToursContext _dbContext;

        public TourDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(int id)
        {
            return _dbContext.Tours.Any(t => t.Id == id);
        }

        public List<Tour> GetToursByAuthor(int id)
        {
            return _dbContext.Tours
                .Include(t => t.Equipment)
                .Where(t => t.AuthorId == id)
                .ToList();
        }
    }
}
