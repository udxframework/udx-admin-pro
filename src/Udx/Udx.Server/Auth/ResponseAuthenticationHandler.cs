using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using Udx.Extensions;
using Udx.Models;
using StatusCodes = Udx.Enums.StatusCodes;

namespace Udx.Auth;
/// <summary>
/// 响应认证处理器
/// </summary>
public class ResponseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ResponseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock
    ) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
        await Response.WriteAsync(JsonConvert.SerializeObject(
                new DataResponse<object>
                {
                Successful = false,
                Code = ((int)StatusCodes.Status403Forbidden).ToString(),
                    Message = StatusCodes.Status403Forbidden.ToDescription()
            }
        ));
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden;
        await Response.WriteAsync(JsonConvert.SerializeObject(
            new DataResponse<object>
            {
                Successful = false,
                Code = ((int)StatusCodes.Status403Forbidden).ToString(),
                Message = StatusCodes.Status403Forbidden.ToDescription()
            }
        ));
    }
}