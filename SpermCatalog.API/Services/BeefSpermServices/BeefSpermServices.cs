using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Services.BeefSpermServices
{
    public class BeefSpermServices : IBeefSpermServices
    {


        private readonly IBeefRepository _beefRepo;
        private IMapper _mapper;

        public BeefSpermServices(IBeefRepository beefRepository, IMapper mapper)
        {
            _beefRepo = beefRepository;
            _mapper = mapper;
        }


        public void AddBeefSperms(List<BeefSpermCsvDTO> beefSpermCsvDTOs)
        {
            var beefSpermList = _mapper.Map<List<BeefSperm>>(beefSpermCsvDTOs);
            _beefRepo.AddBeefSpermsListAsync(beefSpermList);
        }



        public List<BeefResponseDTO> BeefSpermListResponse()
        {
            var beefSpermList = _beefRepo.GetBeefSpermsAsync().Result;
            var responseDTO = _mapper.Map<List<BeefResponseDTO>>(beefSpermList);
            return responseDTO;
        }
    }
}
