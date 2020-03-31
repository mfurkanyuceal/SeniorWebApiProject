namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UpdateAddressRequest
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
    }
}