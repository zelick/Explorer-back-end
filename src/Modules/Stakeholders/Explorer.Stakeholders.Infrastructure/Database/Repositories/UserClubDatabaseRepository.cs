using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
	public class UserClubDatabaseRepository : IUserClubRepository
	{
		private readonly StakeholdersContext _dbContext;
		public UserClubDatabaseRepository(StakeholdersContext dbContext) 
		{
			_dbContext = dbContext;
		}

		public UserClub AddUserToClub(long userId, long clubId)
		{
			var userClub = new UserClub(clubId, userId);
			_dbContext.UserClubs.Add(userClub);
			_dbContext.SaveChanges();
			return userClub;
		}

		public UserClub RemoveUserFromClub(long userId, long clubId)
		{
			var userClub = new UserClub(clubId, userId);
			_dbContext.UserClubs.Remove(userClub);
			_dbContext.SaveChanges();
			return userClub;
		}
	}
}
