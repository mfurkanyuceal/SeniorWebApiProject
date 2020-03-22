using System.ComponentModel.DataAnnotations;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Domain.Swap
{
    public class Swap
    {
        [Key] public string Id { get; set; }

        public AppUser SenderUser { get; set; }

        public AppUser RecieverUser { get; set; }

        public string SwapDate { get; set; }

        public string SendedDate { get; set; }

        public string AcceptedDate { get; set; }

        public int Rate { get; set; }

        public bool IsDone { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }

        public Address Address { get; set; }
    }
}