using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
	public interface IUserClubRepository
	{
		UserClub RemoveUserFromClub(long userId, long clubId);
		UserClub AddUserToClub(long userId, long clubId);
	}
}
