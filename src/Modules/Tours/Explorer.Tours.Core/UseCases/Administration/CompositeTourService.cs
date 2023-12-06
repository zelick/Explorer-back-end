using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CompositeTourService : CrudService<CompositeTourCreationDto, CompositeTour>, ICompositeTourService
    {
        private ICompositeTourRepository _compositeTourRepository;
        private ITourRepository _tourRepository;
        private EquipmentMapper _eqipmentMapper = new EquipmentMapper();
        private CheckpointMapper _checkpointMapper = new CheckpointMapper();
        public CompositeTourService(ICompositeTourRepository CompTourRepository, IMapper mapper, ITourRepository tourRepository) : base(CompTourRepository, mapper)
        {
            _compositeTourRepository = CompTourRepository;
            _tourRepository = tourRepository;

        }

        public Result<CompositeTourCreationDto> Create(CompositeTourCreationDto entity)
        {
            if (entity.TourIds is null || !entity.TourIds.Any()) return Result.Fail(FailureCode.InvalidArgument);


            try
            {
                var result = CrudRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<CompositeTourDto> GetDetailed(int id)
        {

            try
            {
                var result = CrudRepository.Get(id);
                return MapDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<CompositeTourDto>> GetDetailedPaged(int page, int pageSize)
        {
            var result = CrudRepository.GetPaged(page, pageSize);
            var ret = new List<CompositeTourDto>();



            foreach (var creationDto in result.Results)
            {
                if (creationDto.TourIds is null || !creationDto.TourIds.Any()) continue;

                var dto = MapDto(creationDto);

                ret.Add(dto);
            }

            return new PagedResult<CompositeTourDto>(ret,ret.Count());
        }


        private CompositeTourDto MapDto( CompositeTour creationDto)
        {
            var eqipment = new List<EquipmentDto>();
            var checkpoints = new List<CheckpointDto>();
            var demandigness = new List<int>();

            foreach (var id in creationDto.TourIds)
            {

                var tour = _tourRepository.Get(id);
                foreach (var eq in tour.Equipment)
                {
                    var idfix = (int)eq.Id;
                    var eqipmentDto = _eqipmentMapper.createDto(eq);
                    eqipmentDto.Id = idfix;
                    eqipment.Add(eqipmentDto);
                }
                foreach (var ch in tour.Checkpoints)
                {
                    checkpoints.Add(_checkpointMapper.CreateDto(ch));
                }
                demandigness.Add((int)tour.DemandignessLevel);


            }

            eqipment = eqipment.GroupBy(e => e.Id).Select(group => group.First()).ToList();
            double averageDemandingness = demandigness.Count > 0 ? demandigness.Average(): 0;
            Demandigness averageDemandingnessEnum = (Demandigness)Convert.ToInt32(averageDemandingness);
            return new CompositeTourDto(creationDto.Id, creationDto.Name, creationDto.Description, creationDto.OwnerId, averageDemandingnessEnum.ToString(), eqipment, checkpoints) { };
        }
    }
}
