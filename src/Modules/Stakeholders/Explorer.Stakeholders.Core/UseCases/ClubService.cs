using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        public ClubService(IClubRepository repository, IMapper mapper) : base(repository, mapper) {

            _clubRepository = repository;
        }

        public Result<ClubDto> GetClubWithUsers(int id) 
        {
            try {
                var result = _clubRepository.GetClubWithUsers(id);
                return MapToDto(result); 
            }
            catch (KeyNotFoundException e) 
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            } 
        }
    }
}