using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserUpdateRequest
    {
        public AppUser User { get; set; }
    }
}