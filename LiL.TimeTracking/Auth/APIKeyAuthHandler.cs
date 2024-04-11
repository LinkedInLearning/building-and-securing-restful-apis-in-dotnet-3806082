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
        return Task.FromResult(AuthenticateResult.Fail("Not implemented"));
    }
}