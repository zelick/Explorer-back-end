using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
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
    public class AccountsManagementService : BaseService<UserDto, User>, IAccountsManagementService
    {
        private readonly ICrudRepository<User> _repository;
        private readonly IPersonRepository _personRepository;

        public AccountsManagementService(ICrudRepository<User> repository, IMapper mapper, IPersonRepository personRepository) : base(mapper)
        {
            _repository = repository;
            _personRepository = personRepository;
        }

        public Result<PagedResult<UserDto>> GetPaged(int page, int pageSize)
        {
            try
            {
                var usersResult = _repository.GetPaged(page, pageSize);

                if (usersResult == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("No users found.");
                }

                var users = usersResult.Results;
                var userDtos = users.Select(user => MapToDto(user)).ToList();

                foreach (var user in userDtos)
                {
                    if (user.Id != null)
                    {
                        var person = _personRepository.Get((int)user.Id);
                        if (person != null)
                        {
                            user.Email = person.Email;
                        }
                    }
                }

                var updatedPagedResult = new PagedResult<UserDto>(userDtos, usersResult.TotalCount);
                return Result.Ok(updatedPagedResult);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<UserDto> Block(int id)
        {
            try
            {
                UserDto entity = MapToDto(_repository.Get(id));
                entity.IsActive = false;
                var deletedPerson = _personRepository.Get(id);
                _repository.Delete(id);
                var result = _repository.Create(MapToDomain(entity));
                _personRepository.Create(deletedPerson);
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