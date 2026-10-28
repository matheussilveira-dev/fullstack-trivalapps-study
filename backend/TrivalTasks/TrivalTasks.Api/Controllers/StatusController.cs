using Microsoft.AspNetCore.Mvc;

namespace TrivalTasks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStatus()
    {
        return Ok(new
        {
            message = "Backend .NET funcionando corretamente"
        });
    }
}