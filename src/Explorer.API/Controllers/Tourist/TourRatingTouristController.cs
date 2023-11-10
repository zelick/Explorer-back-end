using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-rating")]
    public class TourRatingTouristController : BaseApiController
    {
        private readonly ITourRatingService _tourRatingService;
        private readonly ICustomerService _customerService;

        public TourRatingTouristController(ITourRatingService tourRatingService, ICustomerService customerService)
        {
            _tourRatingService = tourRatingService;
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourRatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourRatingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

		[HttpGet("getTourRating/{id:int}")]
		public ActionResult<TourRatingDto> GetTourRating(int id)
		{
			var result = _tourRatingService.GetTourRating(id);
			return CreateResponse(result);
		}

		[HttpPost]
        public ActionResult<TourRatingDto> Create([FromBody] TourRatingDto tourRating)
        {
            if (tourRating.TourId == 0 || tourRating.TouristId == 0 || tourRating.Rating == 0 || tourRating.Rating > 5)
            {
                return BadRequest("Fill all the fields properly.");
            }

            List<long> customerPurchasedToursIds = _customerService.getCustomersPurchasedTours(tourRating.TouristId);

            if (!customerPurchasedToursIds.Contains(tourRating.TourId))
            {
                return BadRequest("The tour is not included in the purchased tours of this tourist");
            }

            TourExecution tourExecution = new TourExecution(tourRating.TouristId, tourRating.TourId);

            if (!tourExecution.IsTourProgressAbove35Percent())
            {
                return BadRequest("The tour is not completed more than 35 percent, so you cannot leave a review.");
            }

            if (tourExecution.HasOneWeekPassedSinceLastActivity())
            {
                return BadRequest("You cannot leave a review, more than a week has passed since the tour was activated.");
            }

            var result = _tourRatingService.Create(tourRating);
            return CreateResponse(result);

        }

        [HttpPut("{id:int}")]
        public ActionResult<TourRatingDto> Update([FromBody] TourRatingDto tourRating)
        {

            if (tourRating.TourId == 0 || tourRating.TouristId == 0 || tourRating.Rating == 0 || tourRating.Rating > 5)
            {
                return BadRequest("Fill all the fields properly.");
            }

            List<long> customerPurchasedToursIds = _customerService.getCustomersPurchasedTours(tourRating.TouristId);

            if (!customerPurchasedToursIds.Contains(tourRating.TourId))
            {
                return BadRequest("The tour is not included in the purchased tours of this tourist");
            }

            TourExecution tourExecution = new TourExecution(tourRating.TouristId, tourRating.TourId);

            if (!tourExecution.IsTourProgressAbove35Percent())
            {
                return BadRequest("The tour is not completed more than 35 percent, so you cannot edit a review.");
            }

            if (tourExecution.HasOneWeekPassedSinceLastActivity())
            {
                return BadRequest("You cannot edit a review, more than a week has passed since the tour was activated.");
            }

            var result = _tourRatingService.Update(tourRating);
            return CreateResponse(result);

        }
    }
}
