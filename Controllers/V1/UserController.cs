using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Controllers.V1.Requests.User;
using SeniorWepApiProject.Controllers.V1.Responses.Auth;
using SeniorWepApiProject.Controllers.V1.Responses.User;
using SeniorWepApiProject.Domain.AppUserModels;
using SeniorWepApiProject.Services.User;

namespace SeniorWepApiProject.Controllers.V1
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
        }

        [HttpPost(ApiRoutes.User.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }


            var authResponse = await _userService.RegisterAsync(request.UserName, request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.User.Token
            });
        }

        [HttpPost(ApiRoutes.User.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _userService.LoginAsync(request.EmailOrUserName, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.User.Token
            });
        }

        [HttpPut(ApiRoutes.User.Update)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            var updateResponse = await _userService.UpdateAsync(request.User);

            if (!updateResponse.Success)
            {
                return BadRequest(new UpdateFailedResponse
                {
                    Errors = updateResponse.Errors
                });
            }

            return Ok(new UpdateSuccessResponse
            {
                User = updateResponse.User
            });
        }

        [HttpDelete(ApiRoutes.User.Delete)]
        public async Task<IActionResult> Delete([FromBody] UserDeleteRequest request)
        {
            var deleteResponse = await _userService.DeleteAsync(request.User);

            if (!deleteResponse.Success)
            {
                return BadRequest(new DeleteFailedRequest()
                {
                    Errors = deleteResponse.Errors
                });
            }

            return NoContent();
        }

        [HttpGet(ApiRoutes.User.Get)]
        public IActionResult Get(string userId)
        {
            var user = _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet(ApiRoutes.User.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _userService.GetUsersAsync();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost(ApiRoutes.User.UploadPhoto)]
        public async Task<IActionResult> UploadPhoto(AppUser user, IFormFile file)
        {
            try
            {
                var name = file.FileName.Split(".");
                var path = Path.Combine("/home/ubuntu/UsersPhoto", user.Id + "ProfilePhoto." + name[1]);

                user.UserPhotoUrl = user.Id + "ProfilePhoto." + name[1];
                await _userService.UpdateAsync(user);

                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Ok(new {lenth = file.Length, name = file.Name});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpGet(ApiRoutes.User.ProfilePhoto)]
        public IActionResult ProfilePhoto(AppUser user)
        {
            try
            {
                string path = "";

                if (user.UserPhotoUrl.Equals("defaultuser.png"))
                {
                    path = Path.Combine(_environment.WebRootPath, user.UserPhotoUrl);
                }
                else
                {
                    path = Path.Combine("/home/ubuntu/UsersPhoto", user.UserPhotoUrl);
                }

                var image = System.IO.File.OpenRead(path);
                return File(image, "image/jpeg");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}