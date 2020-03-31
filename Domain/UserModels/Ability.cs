using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class Ability
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<UserAbility> UserAbilities { get; set; }
    }
}