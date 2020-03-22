using System.ComponentModel.DataAnnotations;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class Neighborhood
    {
        [Key] public int Id { get; set; }
        public District District { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}