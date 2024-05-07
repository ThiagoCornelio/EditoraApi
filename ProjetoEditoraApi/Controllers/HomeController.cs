using Microsoft.AspNetCore.Mvc;

namespace ProjetoEditoraApi.Controllers;
[ApiController]

public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}
 