using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class MapObjectDatabaseRepository : CrudDatabaseRepository<MapObject, ToursContext>, IMapObjectRepository
    {
        private readonly ToursContext _dbContext;

        public MapObjectDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public MapObject SetPublicStatus(int id)
        {
            var mapObject = _dbContext.MapObjects.FirstOrDefault(o => o.Id == id);
            if (mapObject == null) throw new KeyNotFoundException("Not found " + id);
            mapObject.SetPublicStatus();
            _dbContext.MapObjects.Update(mapObject);
            _dbContext.SaveChanges();
            return mapObject;
        }
    }
}
