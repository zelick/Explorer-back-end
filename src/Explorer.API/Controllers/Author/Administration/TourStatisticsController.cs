using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourStatistics")]
    public class TourStatisticsController : BaseApiController
    {
        private readonly ITourService _tourService; //ovo mozda obrisati
        public TourStatisticsController(ITourService tourService)
        {
            _tourService = tourService;
        }

       

    }
}
