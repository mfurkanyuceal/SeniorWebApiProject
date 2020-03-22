using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;
using SeniorWepApiProject.Options;

namespace SeniorWepApiProject.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
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

            return GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthenticationResult> UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = true,
                    User = user,
                };
            }
            else
            {
                return new AuthenticationResult
                {
                    Errors = new[] {result.Errors.FirstOrDefault()?.Description},
                    Success = false,
                };
            }
        }

        public async Task<AuthenticationResult> DeleteAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = true,
                };
            }
            else
            {
                return new AuthenticationResult
                {
                    Errors = new[] {result.Errors.FirstOrDefault()?.Description},
                    Success = false,
                };
            }
        }

        public AppUser GetUserByIdAsync(string userId)
        {
            var user = _userManager.Users.Include(x => x.Addresses)
                .ThenInclude(add => add.Neighborhood).ThenInclude(add => add.District).ThenInclude(add => add.City)
                .Include(x => x.UserAbilities).ThenInclude(x => x.Ability).Include(x => x.UserFancies)
                .ThenInclude(x => x.Fancy).Include(x => x.OutgoingSwaps).Include(x => x.OutgoingMessages)
                .Include(x => x.InComingSwaps).Include(x => x.InComingMessages)
                .FirstOrDefault(x => x.Id == userId);


            return user;
        }


        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(AppUser user)
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
            user.Token = tokenHandler.WriteToken(token);

            return new AuthenticationResult
            {
                Success = true,
                User = user
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
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = username,
            };


            if (newUser.UserPhotoUrl.Equals(""))
            {
                newUser.UserPhotoUrl = "defaultuser.png";
            }

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthenticationResultForUser(newUser);
        }
    }
}