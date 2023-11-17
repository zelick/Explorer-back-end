using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Shopping;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/customer")]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        private readonly ITourService _tourService;

        public CustomerController(ICustomerService customerService, ITourService tourService)
        {
            _customerService = customerService;
            _tourService = tourService;
        }

        [HttpPost("create")]
        public ActionResult<Customer> Create([FromBody] CustomerDto customer)
        {
            var result = _customerService.Create(customer);
            return CreateResponse(result);
        }

        [HttpPut("{customerId:int}")]
        public ActionResult<CustomerDto> ShoppingCartCheckOut (long customerId)
        {
            var result = _customerService.ShopingCartCheckOut(customerId);
            return CreateResponse(result);
        }

        //umesto id - TourDto
        [HttpGet("cutomersPurchasedToursIds/{touristId:int}")]
        public ActionResult<List<long>> getCustomersPurchasedToursIds(long touristId)
        {
            var result = _customerService.getCustomersPurchasedTours(touristId);

            if(result != null)
            {
                return Ok(result); // Status 200 OK
            }
            else
            {
                return NotFound(); // Status 404 Not Found ili drugi odgovarajući status kod
            }
        }

        [HttpGet("cutomersPurchasedTours/{touristId:int}")]
        public ActionResult<List<PurchasedTourPreviewDto>> getCustomersPurchasedTours(long touristId)
        {
            var result = _customerService.getCustomersPurchasedTours(touristId);

            var tours = _tourService.GetToursByIds(result);

            return CreateResponse(tours);
        }

        [HttpGet("customersPurchasedTourDetails/{purchasedTourId:int}")]
        public ActionResult <PurchasedTourPreviewDto> getCustomersPurchasedTourDetails(long purchasedTourId)
        {
            var result = _tourService.getPurchasedTourById(purchasedTourId);
            return CreateResponse(result);
        }
    }
}