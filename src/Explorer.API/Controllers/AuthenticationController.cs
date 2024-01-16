using Explorer.API.Services;
using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ImageService _imageService;
    private readonly IVerificationService _verificationService;

    public AuthenticationController(IAuthenticationService authenticationService, IVerificationService verificationService)
    {
        _authenticationService = authenticationService;
        _imageService = new ImageService();
        _verificationService = verificationService;
    }

    [HttpPost]
    public ActionResult<AccountRegistrationDto> RegisterTourist([FromForm] AccountRegistrationDto account, IFormFile profilePicture = null)
    {
        if (profilePicture != null)
        {
            var pictureUrl = _imageService.UploadImages(new List<IFormFile> { profilePicture });
            account.ProfilePictureUrl = pictureUrl[0];
        }
        var result = _authenticationService.RegisterTourist(account);
        return CreateResponse(result);
    }


    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }

    [HttpGet("verify/{verificationTokenData}")]
    public ActionResult VerifyUser(string verificationTokenData)
    {
        var result = _verificationService.Verify(verificationTokenData);
        return Redirect("http://localhost:4200/verification-success");
    }

    [HttpGet("verificationStatus/{username}")]
    public ActionResult<bool> IsUserVerified(string username)
    {
        var result = _verificationService.IsUserVerified(username);
        return CreateResponse(result);
    }

    [HttpGet("send-password-reset-email/{username}")]
    public ActionResult<bool> SendPasswordResetEmail(string username)
    {
        var result = _authenticationService.SendPasswordResetEmail(username);
        return CreateResponse(result);
    }
}