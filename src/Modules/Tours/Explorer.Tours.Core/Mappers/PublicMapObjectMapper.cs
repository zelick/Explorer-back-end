using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class PublicMapObjectMapper
    {
        public PublicMapObjectMapper() { }

        public PublicMapObjectDto createDto(MapObjectDto mapObject)
        {
            PublicMapObjectDto publicMapObjectDto = new PublicMapObjectDto();
            publicMapObjectDto.Name = mapObject.Name;
            publicMapObjectDto.Id = Convert.ToInt32(mapObject.Id);
            publicMapObjectDto.Description = mapObject.Description;
            publicMapObjectDto.Category = mapObject.Category.ToString();
            publicMapObjectDto.PictureURL = mapObject.PictureURL;
            publicMapObjectDto.Latitude = mapObject.Latitude;
            publicMapObjectDto.Longitude = mapObject.Longitude;

            return publicMapObjectDto;

        }
        public List<PublicMapObjectDto> createListDto(List<MapObjectDto> mapObjectList)
        {
            List<PublicMapObjectDto> result = new List<PublicMapObjectDto>();
            foreach (MapObjectDto mapObject in mapObjectList)
            {
                result.Add(createDto(mapObject));
            }
            return result;
        }
    }
}
