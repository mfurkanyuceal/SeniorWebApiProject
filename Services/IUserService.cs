using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest request);
        Task<AuthenticationResult> LoginAsync(string emailOrUserName, string password);
        Task<AuthenticationResult> DeleteAsync(AppUser user);
        Task<AuthenticationResult> UpdateAsync(AppUser user);
        AppUser GetUserById(string userId);
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
        Task<List<AppUser>> GetUsersAsync();
        Task<AuthenticationResult> RefreshTokenAsync(string requestToken, string requestRefreshToken);
        Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string modelToken, string modelPassword);
        Task<AppUser> GetUserByUserNameAsync(string username);
    }
}