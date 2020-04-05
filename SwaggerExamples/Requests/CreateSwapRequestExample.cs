using System;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Domain;
using Swashbuckle.AspNetCore.Filters;

namespace SeniorWepApiProject.SwaggerExamples.Requests
{
    public class CreateSwapRequestExample : IExamplesProvider<CreateSwapRequest>
    {
        public CreateSwapRequest GetExamples()
        {
            return new CreateSwapRequest
            {
                SwapDate = DateTime.Now.ToString("F"),
                Address = new Address
                {
                    Description = "Description",
                    Latitude = 15.0001,
                    Longitude = 15.0002
                },
                RecieverUser = new AppUser(),
                SenderUser = new AppUser()
            };
        }
    }
}