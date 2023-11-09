using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>, ICheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        public CheckpointService(ICheckpointRepository repository, IMapper mapper) : base(repository, mapper) 
        {
            _checkpointRepository = repository;
        }
        public Result<PagedResult<CheckpointDto>> GetPagedByTour(int page, int pageSize, int id)
        {
            try
            {
                return MapToDto(_checkpointRepository.GetPagedByTour(page, pageSize, id));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<CheckpointDto> CreateChechpointSecreat(CheckpointSecretDto secret,int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.CreateCheckpointSecret(secret.Description, secret.Pictures)));
        }

        public Result<CheckpointDto> UpdateChechpointSecreat(CheckpointSecretDto secret, int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.UpdateCheckpointSecret(secret.Description, secret.Pictures)));
        }
        public Result<CheckpointDto> DeleteChechpointSecreat(int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.DeleteCheckpointSecret()));
        }

        public Result<CheckpointDto> Get(int id)
        {
            return MapToDto(_checkpointRepository.Get(id));
        }
    }
}
