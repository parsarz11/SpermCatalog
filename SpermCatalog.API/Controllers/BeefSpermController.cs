using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Beef/")]
    [ApiController]
    public class BeefSpermController : ControllerBase
    {
        private readonly IFilterServices _SpermFilter;
        public BeefSpermController(IFilterServices spermFilter)
        {

            _SpermFilter = spermFilter;
        }

        [HttpGet("List")]
        public IActionResult GetListOfBeefSperms([FromQuery]BeefFilterDTO? filterDTO)
        {
            var result = _SpermFilter.FilterBeefSperms(filterDTO);
            
            return Ok(result);
        }
    }
}
