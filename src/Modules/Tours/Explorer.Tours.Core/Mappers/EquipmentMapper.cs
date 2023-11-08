using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class EquipmentMapper
    {
        public EquipmentMapper() { }

        public EquipmentDto createDto(Equipment equipment)
        {
            EquipmentDto equipmentDto = new EquipmentDto();
            equipmentDto.Description = equipment.Description;
            equipmentDto.Name = equipment.Name;
            equipmentDto.Id=equipmentDto.Id;
            return equipmentDto;

        }
        public List<EquipmentDto> createListDto(List<Equipment> equipmentList)
        {
            List<EquipmentDto> result= new List<EquipmentDto>();
            foreach(Equipment equipment in equipmentList)
            {
                result.Add(createDto(equipment));
            }
            return result;
        }

    }
}
