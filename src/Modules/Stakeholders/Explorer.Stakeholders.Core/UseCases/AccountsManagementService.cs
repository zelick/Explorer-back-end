using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AccountsManagementService : CrudService<UserDto, User>, IAccountsManagementService
    {
        public AccountsManagementService(ICrudRepository<User> repository, IMapper mapper): base(repository, mapper) { }

        public Result<UserDto> Block(int id)
        {
            try
            {
                UserDto entity = MapToDto(CrudRepository.Get(id));
                entity.IsActive = false;
                var result = CrudRepository.Update(MapToDomain(entity));
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
    }
}
