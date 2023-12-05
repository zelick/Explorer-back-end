using System.Diagnostics.Metrics;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService : CrudService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
    {
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;
        private readonly IMapper _mapper;
        private readonly IInternalShoppingService _shoppingService;
        private readonly IInternalCheckpointService _internalCheckpointService;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IInternalTouristService _internalTouristService;
        private readonly ICrudRepository<SocialEncounter> _socialEncounterRepository;
        public EncounterExecutionService(IEncounterExecutionRepository encounterExecutionRepository, IMapper mapper, IInternalShoppingService shoppingService, IInternalCheckpointService internalCheckpointService, IEncounterRepository encounterRepository, ICrudRepository<SocialEncounter> socialEncounterRepository, IInternalTouristService internalTouristService) : base(encounterExecutionRepository, mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
            _mapper = mapper;
            _shoppingService = shoppingService;
            _internalCheckpointService = internalCheckpointService;
            _encounterRepository = encounterRepository;
            _socialEncounterRepository = socialEncounterRepository;
            _internalTouristService = internalTouristService;
        }

        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecutionDto, long touristId)
        {
            EncounterExecution result;
            EncounterExecution encounterExecution = new EncounterExecution();

            try
            {
                encounterExecution = _mapper.Map<EncounterExecutionDto, EncounterExecution>(encounterExecutionDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (touristId != encounterExecution.TouristId)
                return Result.Fail(FailureCode.Forbidden);

            try
            {
                encounterExecution.Validate();
                result = _encounterExecutionRepository.Create(encounterExecution);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            return MapToDto(result);
        }

        public Result<EncounterExecutionDto> Get(int id)
        {
            try
            {
                var result = _encounterExecutionRepository.Get(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecutionDto, long touristId)
        {
            EncounterExecution encounterExecution = MapToDomain(encounterExecutionDto);
            if (touristId != encounterExecution.TouristId)
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not tourist enounter execution!");
            try
            {
                var result = CrudRepository.Update(encounterExecution);
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
        public Result Delete(int id, long touristId)
        {
            EncounterExecution encounterExecution;
            try
            {
                encounterExecution = _encounterExecutionRepository.Get(id);

                if (touristId != encounterExecution.TouristId)
                    return Result.Fail(FailureCode.Forbidden);

                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<EncounterExecutionDto>> GetAllByTourist(int touristId, int page, int pageSize)
        {
            try
            {
                var result = _encounterExecutionRepository.GetAllByTourist(touristId);
                var paged = new PagedResult<EncounterExecution>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<PagedResult<EncounterExecutionDto>> GetAllCompletedByTourist(int touristId, int page, int pageSize)
        {
            try
            {
                var result = _encounterExecutionRepository.GetAllCompletedByTourist(touristId);
                var paged = new PagedResult<EncounterExecution>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterExecutionDto> Activate(int touristId, double touristLatitude, double touristLongitude, int encounterId)
        {
            try
            {
                var execution = _encounterExecutionRepository.GetByEncounterAndTourist(touristId, encounterId);
                if(execution.Status == EncounterExecutionStatus.Completed)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Encounter already completed");
                if (execution.IsInRange(touristLatitude, touristLongitude))
                {
                    execution.Activate();
                    execution = _encounterExecutionRepository.Update(execution);
                    return MapToDto(execution);
                }
                return Result.Fail(FailureCode.InvalidArgument).WithError("Tourist not in range");
            }
                
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

       
        public Result<EncounterExecutionDto> GetVisibleByTour(int tourId, double touristLongitude, double touristLatitude, int touristId)
        {
            try
            {
                List<long> encounterIds = _internalCheckpointService.GetEncountersByTour(tourId).Value;
                List<Encounter> encounters = _encounterRepository.GetByIds(encounterIds);
                if(encounters.Count > 0)
                {
                    Encounter closestEncounter = encounters.Find(e => e.IsCloseEnough(touristLongitude, touristLatitude) == true);
                    
                    if(closestEncounter == null) return Result.Fail(FailureCode.InvalidArgument).WithError("No near encounter");
                    
                    double best_distance = closestEncounter.GetDistanceFromEncounter(touristLongitude, touristLatitude); 
                    foreach(Encounter encounter in encounters)
                    {
                        if(encounter.GetDistanceFromEncounter(touristLongitude, touristLatitude) < best_distance && encounter.IsCloseEnough(touristLongitude, touristLatitude))
                        {
                            best_distance = encounter.GetDistanceFromEncounter(touristLongitude, touristLatitude);
                            closestEncounter = encounter;
                        }
                    }
                    var encounterDto = new EncounterExecutionDto();
                    if (_encounterExecutionRepository.GetByEncounterAndTourist(touristId, closestEncounter.Id) == null)
                    {
                        CreateNewEcounterExecution(touristLongitude, touristLatitude, touristId, closestEncounter, encounterDto);
                    }
                    else
                    {
                        encounterDto = MapToDto(_encounterExecutionRepository.GetByEncounterAndTourist(touristId, closestEncounter.Id));
                    }
                    return encounterDto;
                }
                return Result.Fail(FailureCode.InvalidArgument).WithError("No near encounter");
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

        private void CreateNewEcounterExecution(double touristLongitude, double touristLatitude, int touristId, Encounter encounter, EncounterExecutionDto encounterDto)
        {
            encounterDto.EncounterId = encounter.Id;
            encounterDto.TouristId = touristId;
            encounterDto.Status = "Pending";
            encounterDto.TouristLongitute = touristLongitude;
            encounterDto.TouristLatitude = touristLatitude;
            encounterDto.StartTime = DateTime.UtcNow;
            var encounterExecution = MapToDomain(encounterDto);
            encounterExecution.Validate();
            _encounterExecutionRepository.Create(encounterExecution);
        }

        public Result<EncounterExecutionDto> CheckIfInRange(int id, double touristLongitude, double touristLatitude, int touristId)
        {
            try
            {
                var oldExecution = _encounterExecutionRepository.Get(id);
                SocialEncounter result = _socialEncounterRepository.Get(oldExecution.EncounterId);
                var numberOfTourists = result.CheckIfInRange(touristLongitude, touristLatitude, touristId);
                _socialEncounterRepository.Update(result);
                if (result.IsRequiredPeopleNumber())
                {
                    var execution = CompleteExecusion(id, touristId);
                    if (execution.IsSuccess)
                        return execution;
                }
                return MapToDto(oldExecution);
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

        public Result<List<EncounterExecutionDto>> GetActiveByTour(int touristId, int tourId)
        {
            try
            {
                var result = _encounterExecutionRepository.GetActiveByTourist(touristId);
                List<long> encountersIds = _internalCheckpointService.GetEncountersByTour(tourId).Value;
                foreach(var r in result) 
                {
                    if (!encountersIds.Contains(r.EncounterId))
                    {
                        result.Remove(r);
                        break;
                    }
                }
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterExecutionDto> GetWithUpdatedLocation(int tourId, int id, double touristLongitude, double touristLatitude, int touristId)
        {
            try
            {
                CheckIfInRange(id, touristLongitude, touristLatitude, touristId);
                return GetVisibleByTour(tourId, touristLongitude, touristLatitude, touristId);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterExecutionDto> CompleteExecusion(long id, long touristId)
        {
            EncounterExecution encounterExecution;
            try
            {
                encounterExecution = _encounterExecutionRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

            if (touristId != encounterExecution.TouristId)
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not tourist encounter execution!");

            if (encounterExecution.Status != EncounterExecutionStatus.Active)
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not valid status!");
            // TODO - complete location encounter execution
            try
            {
                encounterExecution.Completed();
                //da se turista update xp i level
                _internalTouristService.UpdateTouristXpAndLevel(touristId, encounterExecution.Encounter.XP);
                if (_encounterRepository.Get(encounterExecution.EncounterId).Type == EncounterType.Social)
                    UpdateAllCompletedSocial(encounterExecution.EncounterId);
                var result = CrudRepository.Update(encounterExecution);
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

        private void UpdateAllCompletedSocial(long socialEncounterId)
        {
            List<EncounterExecution> completed = new List<EncounterExecution>(); 
            foreach(var e in _encounterExecutionRepository.GetBySocialEncounter(socialEncounterId))
            {
                e.Completed();
                completed.Add(e);
                //ovde za turistu 
                _internalTouristService.UpdateTouristXpAndLevel(e.TouristId, e.Encounter.XP);
            }
            _encounterExecutionRepository.UpdateRange(completed);
        }
    }
}
