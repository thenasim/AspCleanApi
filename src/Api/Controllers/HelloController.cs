using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class HelloController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Hello()
    {
        await Task.CompletedTask;

        return Ok("Hello World");
    }
}
