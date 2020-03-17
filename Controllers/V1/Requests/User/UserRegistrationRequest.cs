using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.Controllers.V1.Requests.User
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email {get; set;}
        public string UserName {get; set;}
        public string Password { get; set; }
    }
}