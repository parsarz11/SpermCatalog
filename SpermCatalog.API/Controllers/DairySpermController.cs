using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Dairy")]
    [ApiController]
    public class DairySpermController : ControllerBase
    {
        private readonly IDairyServices _DairyServices;
        private IMapper _mapper;
        public DairySpermController(IDairyServices dairyServices,IMapper mapper)
        {
            _DairyServices = dairyServices;
            _mapper = mapper;
        }


        [HttpGet("List")]
        public IActionResult List([FromQuery]DairyFilterDTO? dairyFilterDTO) 
        {
            var spermList = _DairyServices.FilterDairySperms(dairyFilterDTO);
            var mappedList = _mapper.Map<List<DairyResponseDTO>>(spermList);
            return Ok(mappedList);
        }

    }
}
