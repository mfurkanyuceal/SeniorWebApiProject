using System.Collections.Generic;

namespace SeniorWepApiProject.Contracts.V1.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}