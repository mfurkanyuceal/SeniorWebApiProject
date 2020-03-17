namespace SeniorWepApiProject.Controllers.V1.Requests.Location.Address
{
    public class CreateAddressRequest
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        
    }
}