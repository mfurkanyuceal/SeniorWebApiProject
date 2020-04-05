using System.Collections.Generic;

namespace SeniorWepApiProject.Domain
{
    public class AuthenticationResult
    {
        public AppUser User { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string RefreshToken { get; set; }
        public string ConfirmEmailToken { get; set; }
        public string ResetPasswordToken { get; set; }
        public string Token { get; set; }
    }
}