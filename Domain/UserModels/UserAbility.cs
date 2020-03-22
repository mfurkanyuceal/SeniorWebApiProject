using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWebApiProject.Domain.UserModels
{
    public class UserAbility
    {
        public int AbilityId { get; set; }
        public string UserId { get; set; }
        public Ability Ability { get; set; }
        public AppUser User { get; set; }
    }
}