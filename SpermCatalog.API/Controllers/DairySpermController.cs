using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Dairy")]
    [ApiController]
    public class DairySpermController : ControllerBase
    {
        private readonly IDairyServices _DairyServices;

        public DairySpermController(IDairyServices dairyServices)
        {
            _DairyServices = dairyServices;
        }


        [HttpGet("List")]
        public IActionResult List() 
        {
            var result = _DairyServices.DairySpermListResponse();
            return Ok(result);
        }

    }
}
