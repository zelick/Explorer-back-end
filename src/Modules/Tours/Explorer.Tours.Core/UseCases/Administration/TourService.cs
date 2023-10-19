using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        private readonly ITourEquipmentRepository _tourEquipmentRepository;
        private readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository, IMapper mapper, ITourEquipmentRepository tourEquipmentRepository) : base(tourRepository, mapper)
        {
            _tourEquipmentRepository = tourEquipmentRepository;
            _tourRepository = tourRepository;
        }

        public Result<List<TourDto>> GetToursByAuthor(int page, int pageSize, int id) 
        { 
            try
            {
                var result = _tourRepository.GetToursByAuthor(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result AddEquipment(int tourId, int equipmentId)
        {
            var isTourExists = _tourRepository.Exists(tourId);
            if (!isTourExists) return Result.Fail(FailureCode.NotFound);

            //-TO DO make check with equipment repository
            var isEquipmentExists = _tourEquipmentRepository.IsEquipmentExists(equipmentId);
            if (!isEquipmentExists) return Result.Fail(FailureCode.NotFound);

            var isRelationshipExists = _tourEquipmentRepository.Exists(tourId, equipmentId);
            if (isRelationshipExists) return Result.Fail(FailureCode.NotFound);

            _tourEquipmentRepository.AddEquipment(tourId, equipmentId);


            return Result.Ok();

        }

        public Result RemoveEquipment(int tourId, int equipmentId)
        {
            var isRelationshipExists = _tourEquipmentRepository.Exists(tourId, equipmentId);

            if (!isRelationshipExists) return Result.Fail(FailureCode.NotFound);

            _tourEquipmentRepository.RemoveEquipment(tourId, equipmentId);

            return Result.Ok();
        }

    }
}
