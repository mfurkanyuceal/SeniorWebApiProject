using Microsoft.AspNetCore.Mvc;

namespace SeniorWepApiProject.Contracts.V1.Requests.Queries
{
    public class GetAllSwapsQuery
    {
        [FromQuery(Name = "userId")] public string UserId { get; set; }
    }
}