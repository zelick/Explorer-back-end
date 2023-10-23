using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IEquipmentRepository : ICrudRepository<Equipment>
    {
        List<Equipment> GetAvailable(List<long> currentEquipmentIds);
        bool Exists (long id);
    }
}
