using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class Message
    {
        [Key] public string Id { get; set; }

        [Required(ErrorMessage = "Mesajınızı giriniz.")]
        public string Context { get; set; }

        [Required]
        [ForeignKey("senderUserId")]
        public AppUser SenderUser { get; set; }

        public string senderUserId { get; set; }

        [Required]
        [ForeignKey("recieverUserId")]
        public AppUser RecieverUser { get; set; }

        public string recieverUserId { get; set; }


        [Column(TypeName = "Date")] public DateTime SendTime { get; set; }

        [Column(TypeName = "Date")] public DateTime ReadTime { get; set; }

        public bool isDeleted { get; set; }
    }
}