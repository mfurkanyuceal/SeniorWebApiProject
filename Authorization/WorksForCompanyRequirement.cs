using Microsoft.AspNetCore.Authorization;

namespace SeniorWepApiProject.Authorization
{
    public class WorksForCompanyRequirement : IAuthorizationRequirement
    {
        public string DomainName { get; set; }

        public WorksForCompanyRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}