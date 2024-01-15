using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Services.BeefSpermServices
{
    public class BeefSpermServices : IBeefSpermServices
    {


        private readonly IBeefRepository _beefRepository;
        private IMapper _mapper;
        public BeefSpermServices(IBeefRepository beefRepository, IMapper mapper)
        {
            _beefRepository = beefRepository;
            _mapper = mapper;
        }


        public void AddBeefSperms(List<BeefSpermCsvDTO> beefSpermCsvDTOs)
        {
            var beefSpermList = _mapper.Map<List<BeefSperm>>(beefSpermCsvDTOs);
            _beefRepository.AddBeefSpermsListAsync(beefSpermList);
        }
    }
}
