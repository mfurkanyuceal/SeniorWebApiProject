using System.ComponentModel.DataAnnotations;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}