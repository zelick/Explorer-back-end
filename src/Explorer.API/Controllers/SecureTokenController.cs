using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/secureTokens")]
    public class SecureTokenController : BaseApiController
    {
        private readonly ISecureTokenService _secureTokenService;   

        public SecureTokenController(ISecureTokenService secureTokenService)
        {
            _secureTokenService = secureTokenService;
        }

        [HttpGet("get-secure-token/{username}")]
        public ActionResult<SecureTokenDto> GetSecureToken(string username)
        {
            var result = _secureTokenService.GetByUserUsername(username);
            return CreateResponse(result);
        }

        [HttpGet("get-user-id/{tokenData}")]
        public ActionResult<long> GetUserIdByTokenData(string tokenData)
        {
            var result = _secureTokenService.GetUserIdByTokenData(tokenData);
            return CreateResponse(result);
        }
    }
}
