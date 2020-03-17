namespace SeniorWepApiProject.Controllers.V1.Requests.Location.Neighborhood
{
    public class UpdateNeighborhoodRequest
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
    }
}