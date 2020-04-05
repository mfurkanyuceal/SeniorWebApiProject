using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.Domain
{
    public class Message
    {
        [Key] public string Id { get; set; }

        public string Context { get; set; }

        public AppUser SenderUser { get; set; }

        public AppUser RecieverUser { get; set; }


        public string SendTime { get; set; }

        public string ReadTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}