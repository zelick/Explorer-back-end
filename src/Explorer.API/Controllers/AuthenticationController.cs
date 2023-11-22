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
    private readonly IEmailService _emailService;

    public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService)
    {
        _authenticationService = authenticationService;
        _imageService = new ImageService();
        _emailService = emailService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromForm] AccountRegistrationDto account, IFormFile profilePicture = null)
    {
        if (profilePicture != null)
        {
            var pictureUrl = _imageService.UploadImages(new List<IFormFile> { profilePicture });
            account.ProfilePictureUrl = pictureUrl[0];
        }
        _emailService.GenerateVerificationToken(account);
        var result = _authenticationService.RegisterTourist(account);
        _emailService.SendEmail(account);
        return CreateResponse(result);
    }


    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }
}