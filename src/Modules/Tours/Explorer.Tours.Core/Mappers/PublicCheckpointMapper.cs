using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class PublicCheckpointMapper
    {
        public PublicCheckpointMapper() { }

        public PublicCheckpointDto createDto(PublicCheckpoint publicCheckpoint)
        {
            var result = new PublicCheckpointDto();
            result.Id = publicCheckpoint.Id;
            result.TourId = publicCheckpoint.TourId;
            result.Latitude = publicCheckpoint.Latitude;
            result.Longitude = publicCheckpoint.Longitude; 
            result.Name = publicCheckpoint.Name;
            result.Description = publicCheckpoint.Description;
            result.Pictures = publicCheckpoint.Pictures;
            return result;
        }

        public List<PublicCheckpointDto> createDtoList(List<PublicCheckpoint> publicCheckpoints)
        {
            List<PublicCheckpointDto> publicCheckpointsDto = new List<PublicCheckpointDto>();
            foreach (var publicCheckpoint in publicCheckpoints)
            {
                publicCheckpointsDto.Add(createDto(publicCheckpoint));
            }
            return publicCheckpointsDto;
        }
    }
}
