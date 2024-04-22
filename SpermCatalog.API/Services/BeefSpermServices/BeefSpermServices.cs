using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using System.Collections;
using System.Collections.Generic;
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

            response = StringsFilter(response, beefFilterDTO);


            if (!string.IsNullOrEmpty(beefFilterDTO.Range))
            {
                response = StoreAndFilterByRange(response, beefFilterDTO.Range);
            }


            response = OrderingBeefSperms(response, beefFilterDTO.IsDescending, beefFilterDTO.OrderBy);

            if (response.Count <= 0)
            {
                throw new BeefSpermFilterException();
            }

            return response;
        }

        private List<BeefSperm> StringsFilter(List<BeefSperm> beefSperms, BeefFilterDTO beefFilterDTO)
        {
            if (!string.IsNullOrEmpty(beefFilterDTO.Id))
            {
                beefSperms = beefSperms.Where(x => x.Id == beefFilterDTO.Id).ToList();
            }

            if (!string.IsNullOrEmpty(beefFilterDTO.RegNo))
            {
                beefSperms = beefSperms.Where(x => x.RegNo == beefFilterDTO.RegNo).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.BREED))
            {
                beefSperms = beefSperms.Where(x => x.BREED == beefFilterDTO.BREED).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.NAME))
            {
                beefSperms = beefSperms.Where(x => x.NAME == beefFilterDTO.NAME).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.SIRE))
            {
                beefSperms = beefSperms.Where(x => x.SIRE == beefFilterDTO.SIRE).ToList();
            }
            if (!string.IsNullOrEmpty(beefFilterDTO.MGS))
            {
                beefSperms = beefSperms.Where(x => x.MGS == beefFilterDTO.MGS).ToList();
            }

            return beefSperms;
        }

        private List<BeefSperm> StoreAndFilterByRange(List<BeefSperm> beefSperms, string range)
        {
            var deserializedJson = JsonSerializer.Deserialize<RangeListModel>(range);


            foreach (var index in deserializedJson.Filters)
            {

                //save search info
                var rangeFilter = _mapper.Map<RangeFilter>((index, deserializedJson));
                _beefRepo.AddRangeFilterAsync(rangeFilter);

                if (index.MinValue != 0 && index.MaxValue != 0)
                {
                    beefSperms = beefSperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    >= index.MinValue && (double)x.GetType().GetProperty(index.Index).GetValue(x) <= index.MaxValue).ToList();
                }

                else if (index.MinValue != 0)
                {
                    beefSperms = beefSperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    >= index.MinValue).ToList();
                }

                else if (index.MaxValue != 0)
                {
                    beefSperms = beefSperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    <= index.MaxValue).ToList();
                }
            }

            return beefSperms;
        }


        private List<BeefSperm> OrderingBeefSperms(List<BeefSperm> beefSperms,bool isDescending,string orderBy)
        {
            if (isDescending)
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    beefSperms = beefSperms.OrderByDescending(x => x.GetType().GetProperty(orderBy).GetValue(x)).ToList();
                }
                else
                {
                    beefSperms = beefSperms.OrderByDescending(x => x.CustomOrder).ThenBy(x => x.IsNew).ThenByDescending(x => x.SCE).ToList();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    beefSperms = beefSperms.OrderBy(x => x.GetType().GetProperty(orderBy).GetValue(x)).ToList();
                }
            }

            return beefSperms;
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
