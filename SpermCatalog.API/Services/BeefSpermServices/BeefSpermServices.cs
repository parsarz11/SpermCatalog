using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
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


        public void AddRangeBeefSperms(List<BeefSperm> spermList)
        {
            if (spermList == null)
            {
                throw new BeefSpermInvalidDataException();
            }

            _beefRepo.AddRangeBeefSpermsAsync(spermList);
        }

        public void AddBeefSperm(BeefSperm beefSperm)
        {
            if (beefSperm == null)
            {
                throw new BeefSpermInvalidDataException();
            }

            _beefRepo.AddBeefSpermAsync(beefSperm);
        }




        public List<BeefSperm> FilterBeefSperms(BeefFilterDTO beefFilterDTO)
        {
            var response = _beefRepo.GetBeefSpermsAsync().Result.OrderBy(x => x.CustomOrder)
                .ThenByDescending(x => x.IsNew)
                .ThenBy(x => x.SCE)
                .ToList();


            if (response == null)
            {
                throw new BeefSpermNotFoundException();
            }

            if (beefFilterDTO == null)
            {
                return response;
            }

            if(!string.IsNullOrEmpty(beefFilterDTO.Id))
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
                    response = response.OrderByDescending(x => x.CustomOrder).ThenBy(x => x.IsNew).ThenByDescending(x => x.SCE).ToList();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(beefFilterDTO.OrderBy))
                {
                    response = response.OrderBy(x => x.GetType().GetProperty(beefFilterDTO.OrderBy).GetValue(x)).ToList();
                }
            }

            if (response == null)
            {
                throw new BeefSpermFilterException();
            }

            return response;
        }


        public BeefSperm FindSperm(string id)
        {
            var result = _beefRepo.FindBeefSpermAsync(id).Result;

            if (result == null) 
            {
                throw new BeefSpermNotFoundException(id);
            }

            return result;
        }
        
        public void UpdateBeefSperm(BeefSperm beefSperm)
        {
            if (beefSperm == null)
            {
                throw new BeefSpermInvalidDataException();
            }

            _beefRepo.UpdateBeefSpermAsync(beefSperm);
        }

        public void DeleteSperm(string id)
        {
            _beefRepo.DeleteBeefSpermAsync(id);
        }

        public void DeleteAllSperms()
        {
            _beefRepo.DeleteAllBeefSpermsAsync();
        }
    }
}
