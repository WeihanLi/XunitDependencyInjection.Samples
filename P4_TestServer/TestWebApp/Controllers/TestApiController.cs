using Microsoft.AspNetCore.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Route("api/test")]
    public class TestApiController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok(new Result<bool>
            {
                Data = true
            });
        }
    }
}