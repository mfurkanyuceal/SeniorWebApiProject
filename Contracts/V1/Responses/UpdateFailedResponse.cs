using System.Collections.Generic;

namespace SeniorWepApiProject.Contracts.V1.Responses
{
    public class UpdateFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}