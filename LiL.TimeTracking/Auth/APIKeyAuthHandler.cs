using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace LiL.TimeTracking.Auth;

public class APIKeyOptions : AuthenticationSchemeOptions {
    public string? DisplayMessage { get; set; }
}

public class APIKeyAuthHandler : AuthenticationHandler<APIKeyOptions>
{
    private static string[] KEYS = {"123456789", "987654321"};

    public APIKeyAuthHandler(IOptionsMonitor<APIKeyOptions> options, ILoggerFactory logger, UrlEncoder encoder): base(options, logger, encoder)
    {}

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if(!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        string authHeader = Request.Headers[HeaderNames.Authorization];
        string apiKey = authHeader.Split(' ')[1];

        if(KEYS.Contains(apiKey)){
           var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, apiKey)
           } ;
           var identity = new ClaimsIdentity(claims, Scheme.Name);
           var principal = new GenericPrincipal(identity, null);
           var ticket = new AuthenticationTicket(principal, "APIKEY");

           return Task.FromResult(AuthenticateResult.Success(ticket));

        }
        return Task.FromResult(AuthenticateResult.Fail("API Key not valid"));
    }
}