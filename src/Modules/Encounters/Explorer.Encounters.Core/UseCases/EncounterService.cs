using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        public EncounterService(IEncounterRepository encounterRepository, IMapper mapper) : base(encounterRepository, mapper)
        {
            _encounterRepository= encounterRepository;
        }

        public Result<EncounterDto> Create(EncounterDto encounterDto)
        {
            Encounter encounter = MapToDomain(encounterDto);

            try
            {
                var result = CrudRepository.Create(encounter);
                return MapToDto(result);
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
        public Result<EncounterDto> Update(EncounterDto encounterDto)
        {
            Encounter encounter = MapToDomain(encounterDto);
           
            try
            {
                var result = CrudRepository.Update(encounter);
                return MapToDto(result);
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
        public Result Delete(int id)
        {
            Encounter  encounter;
            try
            {
                encounter = _encounterRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterDto> Activate(int id, double touristLongitude, double touristLatitude, int touristId)
        {
            try
            {
                Encounter result = CrudRepository.Get(id);
                bool res = result.ActivateSocial(touristLongitude, touristLatitude, touristId);
                if (!res)
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Not allowed for this tourist");
                }
                return MapToDto(result);
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

        public Result<int> CheckIfInRange(int id, double touristLongitude, double touristLatitude, int touristId)
        {
            try
            {
                Encounter result = CrudRepository.Get(id);
                return result.CheckIfInRange(touristLongitude, touristLatitude, touristId);
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
