using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserDto, User>, IUserService, IInternalUserService
    {
        public UserService(ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) { }

        //ovo mislim da se nigde ne koristi 
       /* public override Result<UserDto> Create(UserDto user)
        {
            try
            {
                var result = CrudRepository.Create(MapToDomain(user));
                //create za Cusomer ako je user role turista 
               /* if (user.Role.Equals(UserRole.Tourist))
                {
                    var customer = new Customer(user.Id);
                    _customerRepository.Create(customer);
                }
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }*/
    }
}
