using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Services.DiarySpermServices
{
    public class DairySpermServices : IDairyServices
    {
        private readonly IDairyRepository _DairyRepo;
        private IMapper _mapper;
        
        public DairySpermServices(IDairyRepository dairyRepo, IMapper mapper)
        {
            _DairyRepo = dairyRepo;
            _mapper = mapper;
        }

        public void AddDairySperms(List<DairySpermCsvDTO> spermDTO)
        {
            var spermsList = _mapper.Map<List<DairySperm>>(spermDTO);
            _DairyRepo.AddDairySpermsListAsync(spermsList);
        }

        public List<DairyResponseDTO> DairySpermListResponse()
        {
            var dairySpermList = _DairyRepo.GetDairySpermsAsync().Result;
            var responseDTO = _mapper.Map<List<DairyResponseDTO>>(dairySpermList);
            return responseDTO;
        }
    }
}
