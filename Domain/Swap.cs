using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWebApiProject.Domain.UserModels;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWepApiProject.Domain
{
    public class Swap
    {
        [Key]
        public string Id { get; set; }
        
        [ForeignKey("senderUserId")] 
        public AppUser SenderUser { get; set; }
        public string senderUserId { get; set; }

        [ForeignKey("recieverUserId")]       
        public AppUser RecieverUser { get; set; }

        public string recieverUserId { get; set; }

        public string SwapDate { get; set; }

        public string sendedDate { get; set; }       

        public string acceptedDate { get; set; }

        public int Rate { get; set; }

        public bool isDone { get; set; }
        public bool isAccepted { get; set; }
        public bool isActive { get; set; }

        [ForeignKey("AddressId")]
        public Address SwapAddress { get; set; }
        public int AddressId { get; set; }
    }
}