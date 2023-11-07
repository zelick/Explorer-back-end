using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        public TourExecutionService(ICrudRepository<TourExecution> repository, IMapper mapper) : base(repository, mapper) { }

        Result<TourExecutionDto> ITourExecutionService.CheckPosition(TouristPositionDto position, long id)
        {
            TourExecution tourExecution = CrudRepository.Get(id);
            TourExecution result=CrudRepository.Update(tourExecution.RegisterActivity(position.Longitude,position.Latitude));
            return MapToDto(result);

        }

      
    }
}
