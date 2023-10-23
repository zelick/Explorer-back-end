namespace Explorer.Tours.Core.Domain
{
    public class TourEquipment
    {
        public long TourId { get; init; }
        public long EquipmentId { get; init; }

        public TourEquipment() { }

        public TourEquipment(long tourId, long equipmentId)
        {
            TourId = tourId;
            EquipmentId = equipmentId;
        }
    }

}
