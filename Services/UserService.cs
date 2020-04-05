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
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.DbContext;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Options;

namespace SeniorWepApiProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _context;
        private readonly IFacebookAuthService _facebookAuthService;

        public UserService(UserManager<AppUser> userManager, JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters, DataContext context,
            IFacebookAuthService facebookAuthService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _facebookAuthService = facebookAuthService;
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

            return await GenerateAuthenticationResultForUserAsync(user);
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
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.User == user);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
            }

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

        public AppUser GetUserById(string userId)
        {
            var user = _userManager.Users.Include(x => x.Addresses)
                .Include(x => x.UserAbilities).ThenInclude(x => x.FieldOfInterest).Include(x => x.UserFancies)
                .ThenInclude(x => x.FieldOfInterest).Include(x => x.OutgoingSwaps).Include(x => x.OutgoingMessages)
                .Include(x => x.InComingSwaps).Include(x => x.InComingMessages)
                .FirstOrDefault(x => x.Id == userId);


            return user;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }


        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"Invalid Token"}};
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult {Errors = new[] {"This token hasn't expired yet"}};
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token is doesn't exist"}};
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has expired"}};
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been invalidated"}};
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been used"}};
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token does not match this JWT"}};
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurtiyAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        private bool IsJwtWithValidSecurtiyAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                User = user,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
                ConfirmEmailToken = emailConfirmToken,
            };
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string modelToken, string modelPassword)
        {
            return await _userManager.ResetPasswordAsync(user, modelToken, modelPassword);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest request)
        {
            var existingUser1 = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser1 != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            }

            var existingUser2 = await _userManager.FindByNameAsync(request.UserName);

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
                Email = request.Email,
                UserName = request.UserName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            List<Address> addresses = new List<Address>();

            foreach (var addressRequest in request.Addresses)
            {
                addresses.Add(new Address
                {
                    Latitude = addressRequest.Latitude,
                    Description = addressRequest.Description,
                    Longitude = addressRequest.Longitude,
                    AppUser = newUser,
                });
            }

            newUser.Addresses = addresses;


            if (string.IsNullOrEmpty(newUser.UserPhotoUrl))
            {
                newUser.UserPhotoUrl = "DefaultUser.png";
            }


            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await GenerateAuthenticationResultForUserAsync(newUser);
        }


        public async Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(accessToken);

            if (!validatedTokenResult.Data.IsValid)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"Invalid Facebook token"}
                };
            }

            var userInfo = await _facebookAuthService.GetUserInfoAsync(accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };

                var createdResult = await _userManager.CreateAsync(newUser);
                if (!createdResult.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] {"Something went wrong"}
                    };
                }

                return await GenerateAuthenticationResultForUserAsync(newUser);
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}