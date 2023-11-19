using Explorer.BuildingBlocks.Core.UseCases;
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
    public class NotificationDatabaseRepository : CrudDatabaseRepository<Notification, StakeholdersContext>, INotificationRepository
    {
        private readonly StakeholdersContext _dbContext;
        public NotificationDatabaseRepository(StakeholdersContext context) : base(context)
        {
            _dbContext = context;
        }


        public Notification Create(string description, long userId, long? foreignId, int type)
        {
            var notif = new Notification(description,
                DateTime.UtcNow, (NotificationType)type, false, userId, foreignId);
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }

        public List<Notification> GetAllByUser(long id)
        {
            return _dbContext.Notifications
                .Where(n => n.UserId == id)
                .ToList();
        }
        public List<Notification> GetUnreadByUser(long id)
        {
            return _dbContext.Notifications
                .Include(n => n.User)
                .Where(n => (n.UserId == id && !n.IsRead))
                .ToList();
        }

        public Notification Get(long id)
        {
            var notif = _dbContext.Notifications
                       .Include(n => n.User)
                       .Where(n => n.Id == id)
                       .FirstOrDefault();
            if (notif == null) throw new KeyNotFoundException("Not found: " + id);
            if (notif.Type != NotificationType.OTHER && notif.ForeignId != null) 
                throw new KeyNotFoundException("Invalid notification: " + id);

            return notif;
        }

        public PagedResult<Notification> GetPaged(int page, int pageSize)
        {
            List<Notification> notifications = new List<Notification>();
            try
            {
                notifications = _dbContext.Notifications
                            .Include(u => u.User).ToList();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return new PagedResult<Notification>(notifications, notifications.Count);
        }

        public Notification Create(Notification notif)
        {
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }

        public Notification Update(Notification notif)
        {
            try
            {
                _dbContext.Notifications.Update(notif);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return notif;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Notifications.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
