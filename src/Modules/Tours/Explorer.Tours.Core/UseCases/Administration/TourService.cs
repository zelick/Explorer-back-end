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
        private readonly IEquipmentRepository _equipmentRepository;

        public TourService(ITourRepository tourRepository, IMapper mapper, ITourEquipmentRepository tourEquipmentRepository, IEquipmentRepository equipmentRepository) : base(tourRepository, mapper)
        {
            _tourEquipmentRepository = tourEquipmentRepository;
            _tourRepository = tourRepository;
            _equipmentRepository = equipmentRepository;
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

        public Result<TourDto> AddEquipment(int tourId, int equipmentId)
        {
            var isTourExists = _tourRepository.Exists(tourId);
            if (!isTourExists) return Result.Fail(FailureCode.NotFound);

            var isEquipmentExists = _equipmentRepository.Exists(equipmentId);
            if (!isEquipmentExists) return Result.Fail(FailureCode.NotFound);

            var isRelationshipExists = _tourEquipmentRepository.Exists(tourId, equipmentId);
            if (isRelationshipExists) return Result.Fail(FailureCode.NotFound);


            var updatedTourId = _tourEquipmentRepository.AddEquipment(tourId, equipmentId).TourId;

            var updatedTour = _tourRepository.Get(updatedTourId);

            return MapToDto(updatedTour);

        }

        public Result<TourDto> RemoveEquipment(int tourId, int equipmentId)
        {
            var isRelationshipExists = _tourEquipmentRepository.Exists(tourId, equipmentId);

            if (!isRelationshipExists) return Result.Fail(FailureCode.NotFound);

            var updatedTourId = _tourEquipmentRepository.RemoveEquipment(tourId, equipmentId).TourId;

            var updatedTour = _tourRepository.Get(updatedTourId);

            return MapToDto(updatedTour);
        }

        public Result<List<TourDto>> GetToursByIds(List<long> tourIds)
        {
            var tours = _tourRepository.GetToursByIds(tourIds);

            return MapToDto(tours);
        }
    }
}
