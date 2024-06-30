using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using static SpermCatalog.API.models.TimeSelectionModel;

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

        [HttpPost("Add")]
        public IActionResult AddDairySperm(AddDairySpermDTO spermDTO)
        {
            var dairySperm = _mapper.Map<DairySperm>(spermDTO);
            _DairyServices.AddDairySperm(dairySperm);
            return Ok();
        }

        [HttpGet("List")]
        public IActionResult List([FromQuery]DairyFilterDTO? dairyFilterDTO) 
        {
            var spermList = _DairyServices.FilterDairySperms(dairyFilterDTO);
            var mappedList = _mapper.Map<List<DairyResponseDTO>>(spermList);
            return Ok(mappedList);
        }


        [HttpGet("Find")]
        public IActionResult FindSperm(string id)
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
        public IActionResult DeleteSperm(string id)
        {
            _DairyServices.DeleteSperm(id);
            return Ok();
        }

        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            _DairyServices.DeleteAllSperms();
            return Ok();
        }

        [HttpGet("GetRangeFilters")]
        public IActionResult GetRangeFilters()
        {
            var rangeFilters = _DairyServices.GetRangeFilters();
            return Ok(rangeFilters);
        }

        [HttpGet("RangeFilterAvg")]
        public IActionResult GetRangeFilterAvg(TimeSelectionEnum timeSelection)
        {
            var rangeFilterAverage = _DairyServices.CalculateRangeFilterAvg(timeSelection);
            return Ok(rangeFilterAverage);
        }

        [HttpGet("RangeFilterSearchCount")]
        public IActionResult GetRangeFilterSearchCount(TimeSelectionEnum timeSelection)
        {
            var searchCount = _DairyServices.CalculateRangeFilterSearchCount(timeSelection);
            return Ok(searchCount);
        }
    }
}
