using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }

    [HttpGet("/ip")]
    public IActionResult Ip()
    {
        var connection = HttpContext.Features.Get<IHttpConnectionFeature>();

        var ipInfo = new
        {
            Local = $"{connection?.LocalIpAddress}:{connection?.LocalPort}",
            Remote = $"{connection?.RemoteIpAddress}:{connection?.RemotePort}",
            ConnectionId = $"{connection?.ConnectionId}"
        };

        return Ok(ipInfo);
    }
}
