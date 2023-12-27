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
    public class SecureTokenService : CrudService<SecureTokenDto, SecureToken>, ISecureTokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecureTokenRepository _secureTokenRepository;

        public SecureTokenService(ICrudRepository<SecureToken> repository, IMapper mapper, IUserRepository userRepository, ISecureTokenRepository secureTokenRepository) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _secureTokenRepository = secureTokenRepository;
        }

        public Result<SecureTokenDto> CreateSecureToken(long userId)
        {
            var user = _userRepository.GetUserById((int) userId);
            if(user == null) { return Result.Fail("User not found"); }
            var secureToken = _secureTokenRepository.CreateSecureToken(userId);
            return MapToDto(secureToken);
        }

        public Result<SecureTokenDto> GetByUserUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return Result.Fail("Not found user with this username: " + username);
            var secureToken = _secureTokenRepository.GetByUserId(user.Id);
            return MapToDto(secureToken);
        }

        public Result<SecureTokenDto> UseSecureToken(long tokenId)
        {
            var token = _secureTokenRepository.UseSecureToken(tokenId);
            return MapToDto(token);
        }
    }
}
