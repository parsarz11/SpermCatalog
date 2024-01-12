using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Beef/[Action]")]
    [ApiController]
    public class BeefSpermController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
