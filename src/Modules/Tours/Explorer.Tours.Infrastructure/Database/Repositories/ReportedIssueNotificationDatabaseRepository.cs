using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ReportedIssueNotificationDatabaseRepository : IReportedIssueNotificationRepository
    {
        private readonly ToursContext _dbContext;
        public ReportedIssueNotificationDatabaseRepository(ToursContext toursContext)
        {
            _dbContext = toursContext;
        }
        public ReportedIssueNotification Create(long userId, long reportedIssueId)
        {
            var notif = new ReportedIssueNotification(
                "New notification for your reported problem!", 
                DateTime.UtcNow, false, userId, reportedIssueId    );
            _dbContext.ReportedIssueNotifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }

        public List<ReportedIssueNotification> GetAllByUser(long id)
        {
            return _dbContext.ReportedIssueNotifications
                //.Include(n => n.ReportedIssue)
                .Where(n => n.UserId == id)
                .ToList();
        }


        public List<ReportedIssueNotification> GetUnreadByUser(long id)
        {
            return _dbContext.ReportedIssueNotifications
                //.Include(n => n.ReportedIssue)
                .Where(n => (n.UserId == id && !n.IsRead))
                .ToList();
        }

        public ReportedIssueNotification Get(long id)
        {
            var notif = _dbContext.ReportedIssueNotifications
                       .Where(n => n.Id == id)
                       //.Include(n => n.ReportedIssue)
                       .FirstOrDefault();
            if (notif == null) throw new KeyNotFoundException("Not found: " + id);

            return notif;
        }

        public PagedResult<ReportedIssueNotification> GetPaged(int page, int pageSize)
        {
            List<ReportedIssueNotification> reportedIssueNotifications = new List<ReportedIssueNotification>();
            try
            {
                reportedIssueNotifications = _dbContext.ReportedIssueNotifications
                            .Include(u => u.ReportedIssue).ToList();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return new PagedResult<ReportedIssueNotification>(reportedIssueNotifications, reportedIssueNotifications.Count);
        }

        public ReportedIssueNotification Create(ReportedIssueNotification notif)
        {
            _dbContext.ReportedIssueNotifications.Add(notif);
            _dbContext.SaveChanges();
            return notif;
        }

        public ReportedIssueNotification Update(ReportedIssueNotification notif)
        {
            try
            {
                _dbContext.ReportedIssueNotifications.Update(notif);
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
            _dbContext.ReportedIssueNotifications.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
