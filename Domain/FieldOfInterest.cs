using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.Domain
{
    public class FieldOfInterest
    {
        [Key] public string Name { get; set; }
        public virtual ICollection<UserFieldOfInterest> UserFieldOfInterests { get; set; }
    }
}