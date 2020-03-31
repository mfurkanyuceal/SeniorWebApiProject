using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Domain
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Swap> Swaps { get; set; }

        public AppUser AppUser { get; set; }
    }
}