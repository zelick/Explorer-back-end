using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class ObjectRequestMapper
    {
        public ObjectRequestMapper() { }

        public ObjectRequestDto createDto(int objectId, int authorId, string status)
        {
            ObjectRequestDto objectRequestDto = new ObjectRequestDto();
            objectRequestDto.MapObjectId = objectId;
            objectRequestDto.AuthorId = authorId;
            objectRequestDto.Status = status;

            return objectRequestDto;

        }
    }
}
