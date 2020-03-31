using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SeniorWebApiProject.Domain.UserModels;

namespace SeniorWepApiProject.Domain.AppUserModels
{
    public class AppUser : IdentityUser<string>
    {
        [Key] public override string Id { get; set; }

        public string FullName { get; set; }
        [NotMapped] public string Token { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string LastLoginDate { get; set; }
        public string LastLogoutDate { get; set; }
        public string RegistrationDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string DeletionDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string UserPhotoUrl { get; set; }

        public virtual ICollection<Swap> InComingSwaps { get; set; }
        public virtual ICollection<Message> InComingMessages { get; set; }
        public virtual ICollection<Swap> OutgoingSwaps { get; set; }
        public virtual ICollection<Message> OutgoingMessages { get; set; }
        public virtual ICollection<UserFancy> UserFancies { get; set; }
        public virtual ICollection<UserAbility> UserAbilities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}