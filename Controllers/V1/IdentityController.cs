using System.Linq;
using System.Threading.Tasks;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Controllers.V1.Requests.User;
using SeniorWepApiProject.Controllers.V1.Responses.Auth;
using SeniorWepApiProject.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRestApi.Controllers.V1 {
    public class IdentityController : Controller {
        private readonly IIdentityService _identityService;

        public IdentityController (IIdentityService identityService) {
            _identityService = identityService;
        }

        [HttpPost (ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register ([FromBody] UserRegistrationRequest request) {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }


            var authResponse = await _identityService.RegisterAsync (request.UserName,request.Email, request.Password);

            if (!authResponse.Success) {
                return BadRequest (new AuthFailedResponse {
                    Errors = authResponse.Errors
                });
            }

            return Ok (new AuthSuccessResponse{
                Token = authResponse.Token
            });
        }

        [HttpPost (ApiRoutes.Identity.Login)] 
        public async Task<IActionResult> Login ([FromBody] UserLoginRequest request) {
            var authResponse = await _identityService.LoginAsync(request.EmailOrUserName, request.Password);

            if (!authResponse.Success) {
                return BadRequest (new AuthFailedResponse {
                    Errors = authResponse.Errors
                });
            }

            return Ok (new AuthSuccessResponse{
                Token = authResponse.Token
            });
        }

    }
}