namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class CreateAddressRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
    }
}