using System.Threading.Tasks;
using SeniorWepApiProject.External.Contracts;

namespace SeniorWepApiProject.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}