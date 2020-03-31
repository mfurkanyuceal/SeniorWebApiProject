using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Contracts.V1.Requests
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