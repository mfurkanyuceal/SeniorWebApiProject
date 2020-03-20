using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class Ability
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserAbility> UserAbilities { get; set; }
    }
}