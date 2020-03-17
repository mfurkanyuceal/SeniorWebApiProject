namespace SeniorWepApiProject.Controllers.V1.Requests.User
{
    public class UserLoginRequest
    {
        public string EmailOrUserName {get; set;}
        public string Password { get; set; }
        
    }
}