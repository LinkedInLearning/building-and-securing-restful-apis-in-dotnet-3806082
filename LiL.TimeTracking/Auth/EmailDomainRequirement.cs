using Microsoft.AspNetCore.Authorization;

namespace LiL.TimeTracking.Auth;

public class EmailDomainRequirement:IAuthorizationRequirement
{
    public EmailDomainRequirement(string domain){
        Domain = domain;
    }

    public string Domain { get; set; }
}