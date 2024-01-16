using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Beef/")]
    [ApiController]
    public class BeefSpermController : ControllerBase
    {
        private readonly IBeefSpermServices _BeefServices;

        public BeefSpermController(IBeefSpermServices beefServices)
        {
            _BeefServices = beefServices;
        }

        [HttpGet("List")]
        public IActionResult GetListOfBeefSperms()
        {
            var result = _BeefServices.BeefSpermListResponse();
            return Ok(result);
        }
    }
}
