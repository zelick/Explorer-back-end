using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ReportedIssuesDatabaseRepository : IReportedIssueRepository
    {
        private readonly ToursContext _dbContext;

        public ReportedIssuesDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ReportedIssue Get(long id)
        {
            var issue = _dbContext.ReportedIssues
                       .Where(b => b.Id == id)
                       .Include(b => b.Tour)
                       .FirstOrDefault();
            if (issue == null) throw new KeyNotFoundException("Not found: " + id);
            return issue;
        }

        public ReportedIssue Resolve(long id)
        {
            var equipment = Get(id);
            try
            {
                equipment.Resolve();
                _dbContext.ReportedIssues.Update(equipment);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return equipment;
        }
        public ReportedIssue AddComment(long id, ReportedIssueComment comment)
        {
            var equipment = Get(id);
            try
            {
                equipment.AddComment(comment);
                _dbContext.ReportedIssues.Update(equipment);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return equipment;
        }

        public PagedResult<ReportedIssue> GetPaged(int page, int pageSize)
        {
            List<ReportedIssue> reportedIssues= new List<ReportedIssue>();
            try
            {
                reportedIssues = _dbContext.ReportedIssues
                            .Include(u => u.Tour).ToList();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return new PagedResult<ReportedIssue>(reportedIssues, reportedIssues.Count);
        }
        public PagedResult<ReportedIssue> GetPagedByAuthor(long id, int page, int pageSize)
        {
            List<ReportedIssue> reportedIssues = new List<ReportedIssue>();
            try
            {
                reportedIssues = _dbContext.ReportedIssues
                            .Include(u => u.Tour).ToList().Where(u=>u.Tour.AuthorId==id).ToList();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return new PagedResult<ReportedIssue>(reportedIssues, reportedIssues.Count);
        }
        public PagedResult<ReportedIssue> GetPagedByTourist(long id, int page, int pageSize)
        {
            List<ReportedIssue> reportedIssues = new List<ReportedIssue>();
            try
            {
                reportedIssues = _dbContext.ReportedIssues
                            .Include(u => u.Tour).ToList().Where(u => u.TouristId == id).ToList();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return new PagedResult<ReportedIssue>(reportedIssues, reportedIssues.Count);
        }
    }
}
