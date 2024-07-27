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
        public async Task AddDairySpermAsync(AddDairySpermDTO spermDTO)
        {
            var dairySperm = _mapper.Map<DairySperm>(spermDTO);
            await _DairyServices.AddDairySpermAsync(dairySperm);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetListAsync([FromQuery]DairyFilterDTO? dairyFilterDTO) 
        {
            var spermList = await _DairyServices.FilterDairySpermsAsync(dairyFilterDTO);

            if (spermList.Count <= 0)
            {
                return Ok(spermList);
            }

            var mappedList = _mapper.Map<List<DairyResponseDTO>>(spermList);
            return Ok(mappedList);
        }


        [HttpGet("Find")]
        public async Task<IActionResult> FindSpermAsync(string id)
        {
            var sperm = await _DairyServices.FindSpermAsync(id);

            if (sperm == null)
            {
                return Ok(new DairyResponseDTO());
            }

            var mappedSperm = _mapper.Map<DairyResponseDTO>(sperm);
            return Ok(mappedSperm);
        }

        [HttpPut("Update")]
        public async Task UpdateSpermAsync(DairySperm dairySperm)
        {
            await _DairyServices.UpdateDairySpermAsync(dairySperm);
        }

        [HttpDelete("Delete")]
        public async Task DeleteSperm(string id)
        {
            await _DairyServices.DeleteSpermAsync(id);
        }

        [HttpDelete("DeleteAll")]
        public async Task DeleteAll()
        {
            await _DairyServices.DeleteAllSpermsAsync();
        }

        [HttpGet("GetRangeFilters")]
        public async Task<IActionResult> GetRangeFilters()
        {
            var rangeFilters = await _DairyServices.GetRangeFiltersAsync();
            return Ok(rangeFilters);
        }

        [HttpGet("RangeFilterAvg")]
        public async Task<IActionResult> GetRangeFilterAvg(TimeSelectionEnum timeSelection)
        {
            var rangeFilterAverage = await _DairyServices.CalculateRangeFilterAvgAsync(timeSelection);
            return Ok(rangeFilterAverage);
        }

        [HttpGet("RangeFilterSearchCount")]
        public async Task<IActionResult> GetRangeFilterSearchCount(TimeSelectionEnum timeSelection)
        {
            var searchCount = await _DairyServices.CalculateRangeFilterSearchCountAsync(timeSelection);
            return Ok(searchCount);
        }
    }
}
