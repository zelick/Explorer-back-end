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
        //-- TO DO: use crud repository to check equipment exist
        bool IsEquipmentExists(int id);
        TourEquipment AddEquipment(int tourId, int equipmentId);
        TourEquipment RemoveEquipment(int tourId, int equipmentId);
    }
}
