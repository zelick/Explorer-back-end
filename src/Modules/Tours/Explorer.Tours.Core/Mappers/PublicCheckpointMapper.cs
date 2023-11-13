using Explorer.Tours.API.Dtos;
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

        public PublicCheckpointDto createDto(CheckpointDto checkpoint)
        {
            PublicCheckpointDto publicCheckpointDto = new PublicCheckpointDto();
            //publicCheckpointDto.Id = checkpoint.Id;
            publicCheckpointDto.Name = checkpoint.Name;
            publicCheckpointDto.Description = checkpoint.Description;
            publicCheckpointDto.Pictures = checkpoint.Pictures;
            publicCheckpointDto.Latitude = checkpoint.Latitude;
            publicCheckpointDto.Longitude = checkpoint.Longitude;

            return publicCheckpointDto;

        }
        public List<PublicCheckpointDto> createListDto(List<CheckpointDto> checkpointList)
        {
            List<PublicCheckpointDto> result = new List<PublicCheckpointDto>();
            foreach (CheckpointDto checkpoint in checkpointList)
            {
                result.Add(createDto(checkpoint));
            }
            return result;
        }
    }
}
