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


        [HttpGet("Find")]
        public IActionResult FindSperm(int id)
        {
            var sperm = _DairyServices.FindSperm(id);
            var mappedSperm = _mapper.Map<DairyResponseDTO>(sperm);
            return Ok(mappedSperm);
        }

        [HttpPut("Update")]
        public IActionResult UpdateSperm(DairySperm dairySperm)
        {
            _DairyServices.UpdateDairySperm(dairySperm);
            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteSperm(int id)
        {
            _DairyServices.DeleteSperm(id);
            return Ok();
        }

        public IActionResult DeleteAll()
        {
            _DairyServices.DeleteAllSperms();
            return Ok();
        }

    }
}
