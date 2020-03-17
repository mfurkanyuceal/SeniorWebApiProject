namespace SeniorWepApiProject.Controllers.V1.Requests.Swap
{
    public class CreateSwapRequest
    {
        public string senderUserId { get; set; }
        public string recieverUserId { get; set; }
        public string SwapDate { get; set; }
        public string sendedDate { get; set; }       
        public string acceptedDate { get; set; }
        public int Rate { get; set; }
        public bool isDone { get; set; }
        public bool isAccepted { get; set; }
        public bool isActive { get; set; }
        public int AddressId { get; set; }
    }
}