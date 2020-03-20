using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWepApiProject.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> LoginAsync(string emailOrUserName, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailOrUserName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(emailOrUserName);

                if (user == null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] {"User does not exist"}
                    };
                }
            }


            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User/password combination is wrong"}
                };
            }

            return GenerateAuthenticationresultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationresultForUser(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<AuthenticationResult> RegisterAsync(string username, string email, string password)
        {
            var existingUser1 = await _userManager.FindByEmailAsync(email);

            if (existingUser1 != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            }

            var existingUser2 = await _userManager.FindByNameAsync(username);

            if (existingUser2 != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this username already exists"}
                };
            }


            var newUser = new AppUser
            {
                Email = email,
                UserName = username
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthenticationresultForUser(newUser);
        }
    }
}