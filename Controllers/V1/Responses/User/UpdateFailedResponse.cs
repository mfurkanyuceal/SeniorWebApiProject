using System.Collections.Generic;

namespace SeniorWepApiProject.Controllers.V1.Responses.User
{
    public class UpdateFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}