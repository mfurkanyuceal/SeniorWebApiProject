using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Controllers.V1.Requests.User
{
    public class UserUpdateRequest
    {
        public AppUser User { get; set; }
    }
}