using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ITourEquipmentRepository _tourEquipmentRepository;

    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper,
        IEquipmentRepository equipmentRepository, ITourEquipmentRepository tourEquipmentRepository) : base(repository, mapper)
    {
        _equipmentRepository = equipmentRepository;
        _tourEquipmentRepository = tourEquipmentRepository;
    }

    public Result<List<EquipmentDto>> GetAvailable(List<long> currentEquipmentIds, int tourId)
    {
        if (currentEquipmentIds.Count > 0)
        {
            var isEquipmentValid = _tourEquipmentRepository.IsEquipmentValid(tourId, currentEquipmentIds);
            if (!isEquipmentValid) return Result.Fail(FailureCode.NotFound);
        }

        var availableEquipment = _equipmentRepository.GetAvailable(currentEquipmentIds);
        return MapToDto(availableEquipment);
    }
}