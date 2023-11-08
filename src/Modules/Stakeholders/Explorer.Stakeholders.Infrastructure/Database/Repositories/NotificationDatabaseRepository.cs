using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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

        public List<Notification> GetAllUnread(int userId)
        {
            return _dbContext.Notifications.Where(n =>  n.UserId == userId && !n.IsRead).ToList();
        }

        public Notification MarkAsRead(int id)
        {
            var notification = _dbContext.Notifications.FirstOrDefault(n => n.Id == id);
            if(notification == null) throw new KeyNotFoundException("Not found " + id);
            notification.MarkAsRead();
            _dbContext.Notifications.Update(notification);
            _dbContext.SaveChanges();
            return notification;
        }
    }
}
