using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Dairy")]
    [ApiController]
    public class DairySpermController : ControllerBase
    {
        private readonly IDairyServices _DairyServices;
        private readonly IFilterServices _FilterServices;
        public DairySpermController(IDairyServices dairyServices, IFilterServices filterServices)
        {
            _DairyServices = dairyServices;
            _FilterServices = filterServices;
        }


        [HttpGet("List")]
        public IActionResult List([FromQuery]DairyFilterDTO? dairyFilterDTO) 
        {
            var result = _FilterServices.FilterDairySperms(dairyFilterDTO);
            return Ok(result);
        }

    }
}
