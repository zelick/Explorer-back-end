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
        private readonly IUserClubRepository _userClubrepository;
        public ClubService(IClubRepository repository, IMapper mapper, IUserClubRepository userClubRepository) : base(repository, mapper) 
        {
            _clubRepository = repository;
            _userClubrepository = userClubRepository;
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

		public Result<ClubDto> RemoveMember(int memberId, int clubId)
        {
			try
			{
				var result = _userClubrepository.RemoveUserFromClub(memberId, clubId);
                var updatedClub = GetClubWithUsers((int)result.ClubId);
				return updatedClub;
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}

		public Result<ClubDto> AddMember(int memberId, int clubId)
		{
			try
			{
				var result = _userClubrepository.AddUserToClub(memberId, clubId);
				var updatedClub = GetClubWithUsers((int)result.ClubId);
				return updatedClub;
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}
	}
}