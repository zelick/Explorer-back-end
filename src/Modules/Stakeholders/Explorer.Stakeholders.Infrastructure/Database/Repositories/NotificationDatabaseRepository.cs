using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class NotificationDatabaseRepository : CrudDatabaseRepository<Notification, StakeholdersContext>, INotificationRepository
    {
        private readonly StakeholdersContext _dbContext;
        public NotificationDatabaseRepository(StakeholdersContext context) : base(context)
        {
            _dbContext = context;
        }
        public Notification CreateRequestNotification(string description, long userId, long? foreignId)
        {
            var notif = new Notification(description, userId, foreignId); 
            notif.RequestType(); // NotificationType.REQUEST
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }
        public Notification CreateReportedIssueNotification(string description, long userId, long? foreignId) 
        {
            var notif = new Notification(description, userId, foreignId);
            notif.ReportedIssueType(); // NotificationType.REPORTED_ISSUE
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }
        public Notification CreateMessageNotification(string description, long userId, long? foreignId)
        {
            var notif = new Notification(description, userId, foreignId);
            notif.MessageType(); // NotificationType.MESSAGE
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }
        public Notification CreateNotification(string description, long userId) 
        {
            var notif = new Notification(description, userId, null); 
            // NotificationType.OTHER
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

        public Notification CreateWalletNotification(string description, long userId)
        {
            var notif = new Notification(description, userId, 0);
            notif.WalletType(); // NotificationType.MESSAGE
            _dbContext.Notifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }
    }
}
