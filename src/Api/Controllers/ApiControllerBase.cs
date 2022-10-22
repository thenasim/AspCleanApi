using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediatr = null!;

    protected ISender Mediator => _mediatr ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
