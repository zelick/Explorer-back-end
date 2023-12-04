using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
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
        private ICompositeTourRepository _repo;
        public CompositeTourService(ICompositeTourRepository tourRepository, IMapper mapper) : base(tourRepository, mapper)
        {
            _repo = tourRepository;
        }

    }
}
