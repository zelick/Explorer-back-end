using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class CheckpointPreviewMapper
    {
       public CheckpointPreviewMapper() { }

       public CheckpointPreviewDto CreateDto(CheckpointPreview checkpoint)
        {
            CheckpointPreviewDto result = new CheckpointPreviewDto();
            result.Pictures = checkpoint.Pictures;
            result.Longitude = checkpoint.Longitude;
            result.Latitude = checkpoint.Latitude;
            result.Name = checkpoint.Name;
            result.Description = checkpoint.Description;
            result.Id = checkpoint.Id;
            result.RequiredTimeInSeconds= checkpoint.RequiredTimeInSeconds;
            return result;

        }

        public List<CheckpointPreviewDto> createListDto(List<CheckpointPreview> checkpointList)
        {
            List<CheckpointPreviewDto> result = new List<CheckpointPreviewDto>();
            foreach (CheckpointPreview cpp in checkpointList)
            {
                result.Add(CreateDto(cpp));
            }
            return result;
        }
    }
}
