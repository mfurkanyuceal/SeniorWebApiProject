namespace SeniorWepApiProject.Domain
{
    public class UserFieldOfInterest
    {
        public string FieldOfInterestName { get; set; }
        public string UserId { get; set; }
        public FieldOfInterest FieldOfInterest { get; set; }
        public AppUser User { get; set; }
    }
}