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

    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper,
        IEquipmentRepository equipmentRepository) : base(repository, mapper)
    {
        _equipmentRepository = equipmentRepository;
    }

    public Result<List<EquipmentDto>> GetAvailable(List<long> currentEquipmentIds)
    {
        var availableEquipment = _equipmentRepository.GetAvailable(currentEquipmentIds);
        return MapToDto(availableEquipment);
    }
}