using Microsoft.AspNetCore.Mvc;

namespace CleanAuthTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working.");
        }
    }
}
