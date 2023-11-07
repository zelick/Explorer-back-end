using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserDatabaseRepository : IUserRepository
{
    private readonly StakeholdersContext _dbContext;

    public UserDatabaseRepository(StakeholdersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }

    public User Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }

    public List<User> GetAll()
    {
        return _dbContext.Users.Include(u => u.Followers).Include(u => u.Followed).Include(u => u.Messages).ToList();
    }

    public User Update(User user)
    {
        try
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            //The code from Microsoft - Resolving concurrency conflicts 
            foreach (var entry in ex.Entries)
            {
                if (entry.Entity is Person)
                {
                    var proposedValues = entry.CurrentValues; //Your proposed changes
                    var databaseValues = entry.GetDatabaseValues(); //Values in the Db

                    foreach (var property in proposedValues.Properties)
                    {
                        var proposedValue = proposedValues[property];
                        var databaseValue = databaseValues[property];

                        // TODO: decide which value should be written to database
                        // proposedValues[property] = <value to be saved>;
                    }

                    // Refresh original values to bypass next concurrency check
                    entry.OriginalValues.SetValues(databaseValues);
                }
                else
                {
                    throw new NotSupportedException(
                        "Don't know how to handle concurrency conflicts for "
                        + entry.Metadata.Name);
                }
            }
        }

        return user;
    }

    public User GetUserById(long id)
    {
        return _dbContext.Users.Include(u => u.Messages)
                                .Include(u => u.Followed)
                                .ThenInclude(u => u.Followers)
                                
                                .FirstOrDefault(user => user.Id == id);
    }
}