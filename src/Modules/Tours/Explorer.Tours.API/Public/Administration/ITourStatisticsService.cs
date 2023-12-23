using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourStatisticsService
    {
        Result<int> GetAuthorsSoldToursNumber(long authorId);
        Result<int> GetAuthorsStartedToursNumber(long authorId);
        Result<int> GetAuthorsFinishedToursNumber(long authorId);
        Result<double> GetAuthorsTourCompletionPercentage(long authorId);
        Result<int> GetToursInCompletionRangeCount(long authorId, double minPercentage, double maxPercentage);
    }
}
