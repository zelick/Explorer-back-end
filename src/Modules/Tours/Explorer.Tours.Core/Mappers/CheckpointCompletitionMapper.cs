using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class CheckpointCompletitionMapper
    {
        public CheckpointCompletitionMapper() { }

        public List<CheckpointCompletitionDto> createDtos(List<CheckpointCompletition> checkpoint)
        {
            List<CheckpointCompletitionDto> dtos = new List<CheckpointCompletitionDto>();
            foreach(var cc in checkpoint)
            {
                CheckpointCompletitionDto dto = new CheckpointCompletitionDto();
                dto.TourExecutionId = cc.TourExecutionId;
                dto.CheckpointId = cc.CheckpointId;
                dto.CompletitionTime = cc.CompletitionTime;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
