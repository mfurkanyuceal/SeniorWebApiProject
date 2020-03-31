using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class CreateSwapRequest
    {
        public AppUser SenderUser { get; set; }
        public AppUser RecieverUser { get; set; }
        public string SwapDate { get; set; }
        public Address Address { get; set; }
    }
}