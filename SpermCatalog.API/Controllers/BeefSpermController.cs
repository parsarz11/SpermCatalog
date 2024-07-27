using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;
using static SpermCatalog.API.models.TimeSelectionModel;

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

        [HttpPost("Add")]
        public async Task AddBeefSperm(AddBeefSpermDTO spermDTO)
        {
            var beefSperm = _mapper.Map<BeefSperm>(spermDTO);
            await _SpermServices.AddBeefSpermAsync(beefSperm);
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetListOfBeefSperms([FromQuery]BeefFilterDTO? filterDTO)
        {
            var spermList = await _SpermServices.FilterBeefSpermsAsync(filterDTO);

            if (spermList.Count <= 0)
            {
                return Ok(spermList);
            }

            var mappedSpermList = _mapper.Map<List<BeefResponseDTO>>(spermList);
            return Ok(mappedSpermList);
        }


        [HttpGet("Find")]
        public async Task<IActionResult> FindSperm(string id)
        {
            var sperm = await _SpermServices.FindSpermAsync(id);

            if (sperm == null)
            {
                
                return Ok(new BeefResponseDTO());
            }

            var mappedSperm = _mapper.Map<BeefResponseDTO>(sperm);
            return Ok(mappedSperm);
        }

        [HttpPut("Update")]
        public async Task UpdateSperm(BeefSperm BeefSperm)
        {
            await _SpermServices.UpdateBeefSpermAsync(BeefSperm);
        }

        [HttpDelete("Delete")]
        public async Task DeleteSperm(string id)
        {
           await _SpermServices.DeleteSpermAsync(id);
        }

        [HttpDelete("DeleteAll")]
        public async Task DeleteAll()
        {
            await _SpermServices.DeleteAllSpermsAsync();
        }

        [HttpGet("GetRangeFilters")]
        public async Task<IActionResult> GetRangeFilters()
        {
            var rangeFilters = await _SpermServices.GetRangeFiltersAsync();
            return Ok(rangeFilters);
        }

        [HttpGet("RangeFilterAvg")]
        public async Task<IActionResult> GetRangeFilterAvg(TimeSelectionEnum timeSelection)
        {
            var rangeFilterAverage = await _SpermServices.CalculateRangeFilterAvgAsync(timeSelection);
            return Ok(rangeFilterAverage);
        }

        [HttpGet("RangeFilterSearchCount")]
        public async Task<IActionResult> GetRangeFilterSearchCount(TimeSelectionEnum timeSelection)
        {
            var searchCount = await _SpermServices.CalculateRangeFilterSearchCountAsync(timeSelection);
            return Ok(searchCount);
        }
    }
}
