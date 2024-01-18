using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;

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


        [HttpGet("Find")]
        public IActionResult FindSperm(int id)
        {
            var sperm = _SpermServices.FindSperm(id);
            var mappedSperm = _mapper.Map<BeefResponseDTO>(sperm);
            return Ok(mappedSperm);
        }

        [HttpPut("Update")]
        public IActionResult UpdateSperm(BeefSperm BeefSperm)
        {
            _SpermServices.UpdateBeefSperm(BeefSperm);
            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteSperm(int id)
        {
            _SpermServices.DeleteSperm(id);
            return Ok();
        }

        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            _SpermServices.DeleteAllSperms();
            return Ok();
        }
    }
}
