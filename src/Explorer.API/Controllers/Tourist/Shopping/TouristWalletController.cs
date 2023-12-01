using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/wallet")]
    public class TouristWalletController : BaseApiController
    {
        private readonly ITouristWalletService _service;

        public TouristWalletController(ITouristWalletService touristWalletService)
        {
            _service = touristWalletService;
        }

        [HttpGet("get-adventure-coins/{userId:long}")]
        public ActionResult<TouristWalletDto> GetAdventureCoins(long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("payment-adventure-coins/{userId:long}/{adventureCoins:int}")]
        public ActionResult<TouristWalletDto> PaymentAdventureCoins(long userId, int adventureCoins)
        {
            throw new NotImplementedException();
        }
    }
}
