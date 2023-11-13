using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class SocialProfileDatabaseRepository : ISocialProfileRepository
    {

        private readonly StakeholdersContext _dbContext;

        public SocialProfileDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SocialProfile Get(long userId)
        {
            var socialProfile = _dbContext.SocialProfiles.Include(sp => sp.Followed)
                                                            .FirstOrDefault(sc => sc.UserId == userId);

            if (socialProfile == null) throw new KeyNotFoundException("Social profile not found.");
            socialProfile.SetFollowers(GetFollowers(socialProfile));

            return socialProfile;
        }

        private List<User> GetFollowers(SocialProfile socialProfile)
        {
            var followersSocialProfiles = _dbContext.SocialProfiles
                .Where(sp => sp.Followed.Any(followedUser => followedUser.Id == socialProfile.UserId))
                .ToList();

            var followerUserIds = followersSocialProfiles.Select(profile => profile.UserId).ToList();

            var followers = _dbContext.Users
                .Where(user => followerUserIds.Contains(user.Id))
                .ToList();

            return followers;
        }


        public SocialProfile Update(SocialProfile profile)
        {
            try
            {
                _dbContext.SocialProfiles.Update(profile);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = entry.GetDatabaseValues();
                var clientValues = entry.Entity;

                entry.OriginalValues.SetValues(databaseValues);

                _dbContext.SaveChanges();
            }
            return profile;
        }
    }
}
