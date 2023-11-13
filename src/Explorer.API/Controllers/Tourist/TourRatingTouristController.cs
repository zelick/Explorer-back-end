using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-rating")]
    public class TourRatingTouristController : BaseApiController
    {
        private readonly ITourRatingService _tourRatingService;
        private readonly ICustomerService _customerService;
        private readonly ITourExecutionRepository _executionRepository;
        private readonly ImageService _imageService;

        public TourRatingTouristController(ITourRatingService tourRatingService, ICustomerService customerService, ITourExecutionRepository executionRepository)
        {
            _tourRatingService = tourRatingService;
            _imageService = new ImageService();
            _customerService = customerService;
            _executionRepository = executionRepository;
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
        public ActionResult<TourRatingDto> Create([FromForm] TourRatingDto tourRating, [FromForm] List<IFormFile>? images = null)
        {
            if (tourRating.TourId == 0 || tourRating.TouristId == 0 || tourRating.Rating == 0 || tourRating.Rating > 5)
            {
                return BadRequest("Fill all the fields properly.");
            }
            List<long> customerPurchasedToursIds = _customerService.getCustomersPurchasedTours(tourRating.TouristId);

            if (!customerPurchasedToursIds.Contains(tourRating.TourId))
            {
                return BadRequest("Unfortunately, you cannot leave a review. This tour is not in your purchased tours.");
            }

            TourExecution tourExecution = _executionRepository.GetExactExecution(tourRating.TourId, tourRating.TouristId);

            if (!tourExecution.IsTourProgressAbove35Percent())
            {
                return BadRequest("Unfortunately, you haven't completed enough of the tour, so you cannot leave a review.");
            }

            if (tourExecution.HasOneWeekPassedSinceLastActivity())
            {
                return BadRequest("You cannot leave a review, more than a week has passed since the tour was activated.");
            }
            // image upload
            if (images != null && images.Any())
            {
                var imageNames = _imageService.UploadImages(images);
                tourRating.ImageNames = imageNames;
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
                return BadRequest("Unfortunately, you cannot leave a review. This tour is not in your purchased tours.");
            }

            TourExecution tourExecution = _executionRepository.GetExactExecution(tourRating.TourId, tourRating.TouristId);

            if (!tourExecution.IsTourProgressAbove35Percent())
            {
                return BadRequest("Unfortunately, you haven't completed enough of the tour, so you cannot leave a review.");
            }

            if (tourExecution.HasOneWeekPassedSinceLastActivity())
            {
                return BadRequest("You cannot leave a review, more than a week has passed since the tour was activated.");
            }

            var result = _tourRatingService.Update(tourRating);
            return CreateResponse(result);

        }
    }
}
