using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class Neighborhood
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        public int DistrictId { get; set; }
        
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}