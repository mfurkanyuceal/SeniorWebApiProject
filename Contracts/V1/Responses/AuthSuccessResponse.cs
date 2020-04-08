namespace SeniorWepApiProject.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}