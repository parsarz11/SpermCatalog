using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using System.Collections;
using System.Text.Json;

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






        public List<BeefSperm> FilterBeefSperms(BeefFilterDTO beefFilterDTO)
        {
            var response = _beefRepo.GetBeefSpermsAsync().Result.OrderBy(x => x.CustomOrder)
                .ThenByDescending(x => x.IsNew)
                .ThenBy(x => x.SCE)
                .ToList();

            if (beefFilterDTO == null)
            {
                return response;
            }

            if(beefFilterDTO.Id != null && beefFilterDTO.Id != 0)
            {
                response = response.Where(x => x.Id == beefFilterDTO.Id).ToList();
            }

            if (!string.IsNullOrEmpty(beefFilterDTO.RegNo))
            {
                response = response.Where(x => x.RegNo == beefFilterDTO.RegNo).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.BREED))
            {
                response = response.Where(x => x.BREED == beefFilterDTO.BREED).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.NAME))
            {
                response = response.Where(x => x.NAME == beefFilterDTO.NAME).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.SIRE))
            {
                response = response.Where(x => x.SIRE == beefFilterDTO.SIRE).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.MGS))
            {
                response = response.Where(x => x.MGS == beefFilterDTO.MGS).ToList();
            }

            if (response.Count <= 0)
            {
                throw new Exception("Data with those sperm filters not found| BeefSpermServices/FilterBeefSperms");
            }

            if (!string.IsNullOrEmpty(beefFilterDTO.Range))
            {

                var range = JsonSerializer.Deserialize<RangeListModel>(beefFilterDTO.Range);


                foreach (var index in range.Filters)
                {

                    if (index.MinValue != 0 && index.MaxValue != 0)
                    {
                        response = response.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                        >= index.MinValue && (double)x.GetType().GetProperty(index.Index).GetValue(x) <= index.MaxValue).ToList();
                    }

                    else if (index.MinValue != 0)
                    {
                        response = response.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                        >= index.MinValue).ToList();
                    }

                    else if (index.MaxValue != 0)
                    {
                        response = response.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                        <= index.MaxValue).ToList();
                    }
                }

                
            }


            if (beefFilterDTO.IsDescending)
            {
                if (!string.IsNullOrEmpty(beefFilterDTO.OrderBy))
                {
                    response = response.OrderByDescending(x => x.GetType().GetProperty(beefFilterDTO.OrderBy).GetValue(x)).ToList();
                }
                else
                {
                    response = response.OrderByDescending(x => x.CustomOrder).ThenBy(x => x.IsNew).ThenByDescending(x => x.PCAR).ThenByDescending(x => x.CR).ToList();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(beefFilterDTO.OrderBy))
                {
                    response = response.OrderBy(x => x.GetType().GetProperty(beefFilterDTO.OrderBy).GetValue(x)).ToList();
                }
            }

            return response;
        }


        public BeefSperm FindSperm(int id)
        {
            return _beefRepo.FindBeefSpermAsync(id).Result;
        }
        
        public void UpdateBeefSperm(BeefSperm beefSperm)
        {
            _beefRepo.UpdateBeefSperm(beefSperm);
        }

        public void DeleteSperm(int id)
        {
            _beefRepo.DeleteBeefSperm(id);
        }

        public void DeleteAllSperms()
        {
            _beefRepo.DeleteAllBeefSpermsAsync();
        }
    }
}
