using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class CheckpointRequestMapper
    {
        public CheckpointRequestMapper() { }

        public CheckpointRequestDto createDto(int checkpointId, int auhtorId, string status)
        {
            CheckpointRequestDto checkpointRequest = new CheckpointRequestDto();
            checkpointRequest.CheckpointId = checkpointId;
            checkpointRequest.AuthorId = auhtorId;
            checkpointRequest.Status = status;
            return checkpointRequest;
        }
    }
}
