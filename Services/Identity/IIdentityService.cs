using System.Threading.Tasks;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services.Identity
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string username, string email, string password);
        Task<AuthenticationResult> LoginAsync(string emailOrUserName, string password);
    }
}