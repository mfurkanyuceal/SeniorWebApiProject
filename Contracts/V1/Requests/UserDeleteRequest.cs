using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserDeleteRequest
    {
        public AppUser User { get; set; }
    }
}