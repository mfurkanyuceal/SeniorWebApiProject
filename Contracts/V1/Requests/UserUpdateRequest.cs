using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserUpdateRequest
    {
        public AppUser User { get; set; }
    }
}