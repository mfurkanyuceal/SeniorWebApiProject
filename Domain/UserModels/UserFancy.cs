using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class UserFancy
    {
        public int FancyId { get; set; }
        public string UserId { get; set; }
        public Fancy Fancy { get; set; }
        public AppUser User { get; set; }



    }
    
    
}