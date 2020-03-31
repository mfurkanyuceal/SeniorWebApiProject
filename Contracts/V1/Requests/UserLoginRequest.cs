namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserLoginRequest
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}