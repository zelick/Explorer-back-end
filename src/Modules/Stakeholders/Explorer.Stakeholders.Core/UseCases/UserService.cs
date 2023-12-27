using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserDto, User>, IUserService, IInternalUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecureTokenRepository _secureTokenRepository;

        public UserService(ICrudRepository<User> repository, IUserRepository userRepository, ISecureTokenRepository secureTokenRepository, IMapper mapper) : base(repository, mapper) 
        {
            _userRepository = userRepository;
            _secureTokenRepository = secureTokenRepository;
        }

        public Result<UserDto> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            return MapToDto(user);
        }

        public Result<UserDto> GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return MapToDto(user);
        }

        public Result<UserDto> UpdatePassword(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            var token = _secureTokenRepository.GetByUserId(user.Id);
            if (token == null) { return Result.Fail("Not found secure token for user with ID: " + user.Id); }
            if(token.ExpiryTime < DateTime.UtcNow)
            {
                return Result.Fail("Secure token is expired.");
            }
            if(token.IsAlreadyUsed)
            {
                return Result.Fail("Secure token is already used.");
            }
            var ret = _userRepository.UpdatePassword(user.Id, password);
            _secureTokenRepository.UseSecureToken(token.Id);
            return MapToDto(ret);
        }

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
