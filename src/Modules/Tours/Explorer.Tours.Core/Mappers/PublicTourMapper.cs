using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class PublicTourMapper
    {
     
        private CheckpointPreviewMapper _previewCheckpointMapper;
        public PublicTourMapper()
        {
            _previewCheckpointMapper = new CheckpointPreviewMapper();
        }

        public PublicTourDto createDto(PublicTour publicTour)
        {

            PublicTourDto result = new PublicTourDto
            {
                Id = publicTour.Id,
                AuthorId = publicTour.AuthorId,
                Name = publicTour.Name,
                Description = publicTour.Description,
                Price = publicTour.Price,
                Tags = publicTour.Tags,
                PreviewCheckpoints = _previewCheckpointMapper.createListDto(publicTour.PreviewCheckpoints)
            };
            return result;
        }

        public List<PublicTourDto> createDtoList(List<PublicTour> publicTours)
        {
            List<PublicTourDto> publicToruDtos = new List<PublicTourDto>();
            foreach (var publicTour in publicTours)
            {
                publicToruDtos.Add(createDto(publicTour));
            }
            return publicToruDtos;
        }

    }
}
