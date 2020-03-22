using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class City
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}