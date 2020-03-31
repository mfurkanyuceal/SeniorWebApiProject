using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress] public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}