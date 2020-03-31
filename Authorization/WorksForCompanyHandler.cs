using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SeniorWepApiProject.Authorization
{
    public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            WorksForCompanyRequirement requirement)
        {
            //Email
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if (userEmailAddress.EndsWith(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}