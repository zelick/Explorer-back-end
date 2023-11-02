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

        public Result<TourDto> Publish(int id)
        {
            var tour = _tourRepository.Get(id);
            tour.Publish();
            var result = _tourRepository.Update(tour);
            return MapToDto(result);
        }

        public Result<TourDto> AddTime(TourTimesDto tourTimesDto, int id)

        {
            var tour = _tourRepository.Get(id);
            tour.ClearTourTimes();
            try
            {
                foreach(var time in tourTimesDto.TourTimes)
                {
                    tour.AddTime(time.TimeInSeconds, time.Distance, time.Transportation);
                }
                var result = _tourRepository.Update(tour);
                return MapToDto(result);
            }
            catch {
                throw new Exception("Invalid tour time");

            }
        }
    }
}
