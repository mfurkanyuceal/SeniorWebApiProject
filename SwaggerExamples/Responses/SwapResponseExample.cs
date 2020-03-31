using System;
using System.Collections.Generic;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;
using Swashbuckle.AspNetCore.Filters;

namespace SeniorWepApiProject.SwaggerExamples.Responses
{
    public class SwapResponseExample : IExamplesProvider<SwapResponse>
    {
        public SwapResponse GetExamples()
        {
            return new SwapResponse
            {
                Id = Guid.NewGuid().ToString(),
                Address = new Address
                {
                    Id = 1,
                    Description = "Description",
                    Latitude = 105.1231412,
                    Longitude = 104.1243123,
                    Swaps = new List<Swap>(),
                    AppUser = new AppUser()
                },
                Rate = 4,
                AcceptedDate = DateTime.Now.ToString("F"),
                IsAccepted = true,
                IsActive = false,
                IsDone = true,
                RecieverUser = new AppUser(),
                SendedDate = DateTime.Now.ToString("F"),
                SenderUser = new AppUser(),
                SwapDate = DateTime.Now.ToString("F")
            };
        }
    }
}