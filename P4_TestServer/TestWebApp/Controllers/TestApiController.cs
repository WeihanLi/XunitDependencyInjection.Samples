using Microsoft.AspNetCore.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Route("api")]
    public class TestApiController : ControllerBase
    {
        [Route("ready")]
        public IActionResult Ready([FromServices] ReadyChecker readChecker)
        {
            if (readChecker.Check())
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("test")]
        public IActionResult Get()
        {
            return Ok(new Result<bool>
            {
                Data = true
            });
        }
    }
}