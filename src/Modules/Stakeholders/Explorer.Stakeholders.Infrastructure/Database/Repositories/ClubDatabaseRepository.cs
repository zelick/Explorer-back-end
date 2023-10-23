using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubDatabaseRepository: CrudDatabaseRepository<Club, StakeholdersContext>,IClubRepository
    {
        
        public ClubDatabaseRepository(StakeholdersContext stakeholdersContext) : base(stakeholdersContext)
        {
         
        }

        public Club GetClubWithUsers(int clubId)
        {
            /*return DbContext.Clubs
                .Include(c => c.Users)
                .Where(c => c.Id == clubId)
                .FirstOrDefault();*/
            var club = DbContext.Clubs
            .Where(c => c.Id == clubId)
            .FirstOrDefault();

            if (club != null)
            {
                var userClubIds = DbContext.UserClubs
                    .Where(uc => uc.ClubId == club.Id)
                    .Select(uc => uc.UserId)
                    .ToList();

                var users = DbContext.Users
                    .Where(u => userClubIds.Contains(u.Id))
                    .ToList();

                club.Users = users;
            }

            return club;
        }
    }
}
