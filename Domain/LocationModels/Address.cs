using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeniorWepApiProject.Domain.AppUserModels;
using SeniorWepApiProject.Domain.Swap;

namespace SeniorWebApiProject.Domain.LocationModels
{
    public class Address
    {
        [Key] public int Id { get; set; }

        [ForeignKey("CityId")] public City City { get; set; }
        public int CityId { get; set; }


        [ForeignKey("DistrictId")] public District District { get; set; }
        public int DistrictId { get; set; }


        [ForeignKey("NeighborhoodId")] public Neighborhood Neighborhood { get; set; }
        public int NeighborhoodId { get; set; }

        public virtual ICollection<Swap> Swaps { get; set; }

        public AppUser AppUser { get; set; }


        public string myToString()
        {
            var str = Neighborhood.Name + " " +
                      Neighborhood.Street + " No:" +
                      Neighborhood.Number + " " +
                      District.Name + "/" +
                      City.Name;

            return str;
        }
    }
}