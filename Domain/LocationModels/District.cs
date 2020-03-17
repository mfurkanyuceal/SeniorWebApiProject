using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

    }
}