using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Migrations
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationController : ControllerBase
    {
        [HttpGet("MigrateUp")]
        public async Task<IActionResult> MigrateUp()
        {

            return Ok();
        }
    }
}
