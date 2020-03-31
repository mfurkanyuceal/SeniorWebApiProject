using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> RegisterAsync(string username, string email, string password);
        Task<AuthenticationResult> LoginAsync(string emailOrUserName, string password);
        Task<AuthenticationResult> DeleteAsync(AppUser user);
        Task<AuthenticationResult> UpdateAsync(AppUser user);
        AppUser GetUserByIdAsync(string userId);
        Task<List<AppUser>> GetUsersAsync();
        Task<AuthenticationResult> RefreshTokenAsync(string requestToken, string requestRefreshToken);
        Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken);
    }
}