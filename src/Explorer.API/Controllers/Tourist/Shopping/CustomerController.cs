using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/customer")]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        private readonly ITourService _tourService;

        public CustomerController(ICustomerService customerService, ITourService tourService)
        {
            _customerService = customerService;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<CustomerDto> GetByUser([FromQuery] int touristId)
        {
            var result = _customerService.GetByUser(touristId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CustomerDto> Create([FromBody] CustomerDto customer)
        {
            var result = _customerService.Create(customer);
            return CreateResponse(result);
        }

        // TODO: check whether this is used on frontend
        [HttpGet("purchased-tour-ids")]
         public ActionResult<List<long>> GetUsersPurchasedToursIds([FromQuery] long touristId)
         {
             var result = _customerService.GetPurchasedToursByUser(touristId);
             return CreateResponse(result);

        }

        [HttpGet("purchased-tours")]
         public ActionResult<List<PurchasedTourPreviewDto>> GetUsersPurchasedTours([FromQuery] long touristId)
         {
             var result = _customerService.GetPurchasedToursByUser(touristId);
             if (result.IsFailed) return BadRequest("Purchased tours not found for the user.");
             var tours = _tourService.GetToursByIds(result.Value);
             return CreateResponse(tours);
         }

         [HttpGet("purchased-tour-details/{purchasedTourId:int}")]
         public ActionResult<PurchasedTourPreviewDto> GetUsersPurchasedTourDetails(long purchasedTourId)
         {
             var result = _tourService.GetPurchasedTourById(purchasedTourId);
             return CreateResponse(result);
         }
    }
}