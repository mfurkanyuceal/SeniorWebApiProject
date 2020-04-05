using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorWepApiProject.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress] public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public List<CreateAddressRequest> Addresses { get; set; }
        public List<CreateFieldOfInterestRequest> Abilities { get; set; }
        public List<CreateFieldOfInterestRequest> Fancies { get; set; }
    }
}