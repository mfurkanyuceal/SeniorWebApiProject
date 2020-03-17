using System.Collections.Generic;

namespace SeniorWepApiProject.Controllers.V1.Responses.Auth
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}