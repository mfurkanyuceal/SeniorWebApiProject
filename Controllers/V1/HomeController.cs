using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Controllers.V1
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFacebookAuthService _facebookAuthService;

        public HomeController(IWebHostEnvironment environment, IUserService userService,
            IFacebookAuthService facebookAuthService)
        {
            _environment = environment;
            _userService = userService;
            _facebookAuthService = facebookAuthService;
        }

        public async Task<IActionResult> LoginWithFacebook([FromBody] UserFacebookAuthRequest request)
        {
            var authResponse = await _userService.LoginWithFacebookAsync(request.AccessToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.User.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}