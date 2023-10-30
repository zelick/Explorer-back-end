using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourEquipmentRepository
    {
        bool Exists(int tourId, int equipmentId);
        bool IsEquipmentValid(int tourId, List<long>  equipmentIds);
        TourEquipment AddEquipment(int tourId, int equipmentId);
        TourEquipment RemoveEquipment(int tourId, int equipmentId);
    }
}
