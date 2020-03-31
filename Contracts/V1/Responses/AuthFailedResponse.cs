using System.Collections.Generic;

namespace SeniorWepApiProject.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}