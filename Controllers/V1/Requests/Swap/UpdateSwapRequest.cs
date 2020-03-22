using SeniorWebApiProject.Domain.LocationModels;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Controllers.V1.Requests.Swap
{
    public class UpdateSwapRequest
    {
        public AppUser SenderUser { get; set; }
        public AppUser RecieverUser { get; set; }
        public string SwapDate { get; set; }
        public string SentDate { get; set; }
        public string AcceptedDate { get; set; }
        public int Rate { get; set; }
        public bool IsDone { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public Address Address { get; set; }
    }
}