using System.Collections.Generic;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Domain
{
    public class AuthenticationResult
    {
        public AppUser User { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}