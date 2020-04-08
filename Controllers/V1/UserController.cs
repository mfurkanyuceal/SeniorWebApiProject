using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Services;
using SeniorWepApiProject.ViewModels;

namespace SeniorWepApiProject.Controllers.V1
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IWebHostEnvironment environment, ILogger<UserController> logger)
        {
            _userService = userService;
            _environment = environment;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.UserRoutes.Register)]
        public async Task<IActionResult> Register([FromServices] IFluentEmail email,
            [FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }


            var authResponse = await _userService.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }


            var confirmationLink = Url.Action("ConfirmEmail", "User"
                , new {userId = authResponse.User.Id, token = authResponse.ConfirmEmailToken}
                , Request.Scheme);

            var emailResponse = await email
                .To(request.Email)
                .Subject("Swap App Email Confirmation")
                .Body("Mail adresinizi onaylamak için linke tıklayınız " +
                      $"{confirmationLink}")
                .SendAsync();

            if (!emailResponse.Successful)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = emailResponse.ErrorMessages
                });
            }

            _logger.Log(LogLevel.Warning, confirmationLink);

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.User.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }

            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"Böyle bir kullanıcı bulunamadı!");
            }

            var result = await _userService.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("E-Posta adresiniz onaylandı!");
            }

            return BadRequest("E-Posta adresiniz onaylanmadı!");
        }

        /// <summary>
        /// Login User Action
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(ApiRoutes.UserRoutes.Login)]
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
                UserId = authResponse.User.Id,
                Token = authResponse.User.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.UserRoutes.LoginWithFacebook)]
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

        [AllowAnonymous]
        [HttpPost(ApiRoutes.UserRoutes.Refresh)]
        public async Task<IActionResult> RefreshLogin([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _userService.RefreshTokenAsync(request.Token, request.RefreshToken);

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

        [HttpPut(ApiRoutes.UserRoutes.Update)]
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

        [HttpDelete(ApiRoutes.UserRoutes.Delete)]
        public async Task<IActionResult> Delete([FromBody] UserDeleteRequest request)
        {
            var user = _userService.GetUserById(request.UserId);


            var deleteResponse = await _userService.DeleteAsync(user);

            if (!deleteResponse.Success)
            {
                return BadRequest(new DeleteFailedRequest()
                {
                    Errors = deleteResponse.Errors
                });
            }

            return NoContent();
        }

        [HttpGet(ApiRoutes.UserRoutes.Get)]
        public IActionResult Get(string userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet(ApiRoutes.UserRoutes.GetAll)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _userService.GetUsersAsync();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost(ApiRoutes.UserRoutes.UploadPhoto)]
        public async Task<IActionResult> UploadPhoto(string userId, IFormFile file)
        {
            try
            {
                var name = file.FileName.Split(".");
                var path = Path.Combine("/home/ubuntu/UsersPhoto", userId + "_ProfilePhoto." + name[1]);

                var user = _userService.GetUserById(userId);

                if (user == null)
                {
                    return BadRequest("Böyle bir kullanıcı bulunamadı");
                }

                user.UserPhotoUrl = userId + "_ProfilePhoto." + name[1];
                await _userService.UpdateAsync(user);

                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Ok("Kullanıcı fotoğrafı güncellendi");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpGet(ApiRoutes.UserRoutes.ProfilePhoto)]
        public IActionResult ProfilePhoto(string userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user == null)
                {
                    return BadRequest("Böyle bir kullanıcı bulunamadı");
                }

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


        [HttpPost(ApiRoutes.UserRoutes.ResetPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordLink([FromServices] IFluentEmail email,
            string emailOrUsername)
        {
            var user = await _userService.GetUserByEmailAsync(emailOrUsername);

            if (user == null)
            {
                user = await _userService.GetUserByUserNameAsync(emailOrUsername);
            }


            if (user == null)
            {
                return BadRequest("Böyle bir kullanıcı bulunmadı!");
            }

            var resetPasswordLink = Url.Action("ResetPassword", "User"
                , new {email = user.Email, token = await _userService.GeneratePasswordResetTokenAsync(user)}
                , Request.Scheme);

            var emailResponse = await email
                .To(user.Email)
                .Subject("Swap App Parola Yenileme")
                .Body("Parolanızı yenilemek için linke tıklayınız " +
                      $"{resetPasswordLink}")
                .SendAsync();

            return Ok("Parola yenileme linki mail adresinize gönderilmiştir.");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Parola veya token geçersiz!");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userService.GetUserByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userService.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok("Parolanız başarıyla güncellendi");
                    }

                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return Ok();
            }

            // Display validation errors if model state is not valid
            return View(model);
        }
    }
}