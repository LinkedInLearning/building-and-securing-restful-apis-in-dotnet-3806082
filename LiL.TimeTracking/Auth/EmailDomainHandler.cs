using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LiL.TimeTracking.Auth;


public class EmailDomainHandler:AuthorizationHandler<EmailDomainRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailDomainRequirement requirement)
    {
        var emailClaim = context.User.FindFirst(
            c=> c.Type == ClaimTypes.Email);

        if(emailClaim is null) {
            return Task.CompletedTask;
        }  

        if(emailClaim.Value.EndsWith(requirement.Domain, 
            StringComparison.InvariantCultureIgnoreCase)){
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
        
    }
}