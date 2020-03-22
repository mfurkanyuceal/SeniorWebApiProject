using System.Collections.Generic;

namespace SeniorWepApiProject.Controllers.V1.Responses.User
{
    public class DeleteFailedRequest
    {
        public IEnumerable<string> Errors { get; set; }
    }
}