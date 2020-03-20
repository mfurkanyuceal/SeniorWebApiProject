using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWebApiProject.Domain.UserModels;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWepApiProject.Domain
{
    public class Swap
    {
        [Key] public string Id { get; set; }

        [ForeignKey("SenderUserId")] public AppUser SenderUser { get; set; }
        public string SenderUserId { get; set; }

        [ForeignKey("RecieverUserId")] public AppUser RecieverUser { get; set; }

        public string RecieverUserId { get; set; }

        public string SwapDate { get; set; }

        public string SendedDate { get; set; }

        public string AcceptedDate { get; set; }

        public int Rate { get; set; }

        public bool IsDone { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("AddressId")] public Address SwapAddress { get; set; }
        public int AddressId { get; set; }
    }
}