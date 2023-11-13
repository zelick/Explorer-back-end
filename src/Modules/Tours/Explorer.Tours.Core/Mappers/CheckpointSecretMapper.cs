using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class CheckpointSecretMapper
    {
        public CheckpointSecretMapper() { }
        public CheckpointSecretDto createDto(string description,List<string> pictures) 
        {
            CheckpointSecretDto dto = new CheckpointSecretDto();
            dto.Description = description;
            dto.Pictures = pictures;
            return dto;
        }


    }
}
