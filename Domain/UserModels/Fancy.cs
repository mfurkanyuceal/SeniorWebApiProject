using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class Fancy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserFancy> UserFancies{ get; set; }

    }
}