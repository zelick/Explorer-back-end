using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
	public interface IUserClubService
	{
		Result<PagedResult<UserClubDto>> GetPaged(int page, int pageSize);
		Result<UserClubDto> Create(UserClubDto userClub);
		Result<UserClubDto> Update(UserClubDto userClub);
		Result Delete(int id);
	}
}
