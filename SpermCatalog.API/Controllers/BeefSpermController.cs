using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;

namespace SpermCatalog.API.Controllers
{
    [Route("api/Beef/")]
    [ApiController]
    public class BeefSpermController : ControllerBase
    {
        private readonly IBeefSpermServices _SpermServices;
        private readonly IMapper _mapper;
        public BeefSpermController(IBeefSpermServices spermServices,IMapper mapper)
        {
            _SpermServices = spermServices;
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult GetListOfBeefSperms([FromQuery]BeefFilterDTO? filterDTO)
        {
            var spermList = _SpermServices.FilterBeefSperms(filterDTO);
            var mappedSpermList = _mapper.Map<List<BeefResponseDTO>>(spermList);
            return Ok(mappedSpermList);
        }
    }
}
