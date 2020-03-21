using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWebApiProject.Domain.UserModels;

namespace SeniorWepApiProject.Domain.IdentityModels
{
    public class AppUser : IdentityUser<string>
    {
        [Key] public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string LastLoginDate { get; set; }
        public string LastLogoutDate { get; set; }
        public string RegistrationDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string DeletionDate { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public string UserPhotoURL { get; set; }
        public virtual ICollection<UserFancy> UserFancies { get; set; }
        public virtual ICollection<UserAbility> UserAbilities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}