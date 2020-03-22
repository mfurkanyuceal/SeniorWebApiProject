using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Controllers.V1.Requests.User
{
    public class UserDeleteRequest
    {
        public AppUser User { get; set; }
    }
}