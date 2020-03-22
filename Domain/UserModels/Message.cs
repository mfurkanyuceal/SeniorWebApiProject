using System.ComponentModel.DataAnnotations;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class Message
    {
        [Key] public string Id { get; set; }

        [Required(ErrorMessage = "Mesajınızı giriniz.")]
        public string Context { get; set; }

        [Required] public AppUser SenderUser { get; set; }


        [Required] public AppUser RecieverUser { get; set; }


        public string SendTime { get; set; }

        public string ReadTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}