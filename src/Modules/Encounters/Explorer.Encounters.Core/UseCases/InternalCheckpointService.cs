using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class InternalCheckpointService: BaseService<CheckpointDto, Checkpoint>, IInternalCheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;

    public InternalCheckpointService(IMapper mapper, ICheckpointRepository checkpointRepository) : base(mapper)
    {
            _checkpointRepository = checkpointRepository;
    }
    public Result<CheckpointDto> UpdateEncounter(int id,long encounterId, bool isSecretPrerequisite)
    {
        Checkpoint checkpoint;
        try
        {
          checkpoint = _checkpointRepository.Get(id);   
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
            catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }

        try
        {
            checkpoint.EncounterId= encounterId;
            checkpoint.IsSecretPrerequisite= isSecretPrerequisite;
            return MapToDto( _checkpointRepository.Update(checkpoint));
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }


        }
    
    }
}
