using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Text.Json;

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

        public void AddRangeDairySperms(List<DairySperm> spermList)
        {
            if (spermList == null)
            {
                throw new DairySpermInvalidException();
            }

            _DairyRepo.AddRangeDairySpermsAsync(spermList);
        }

        public void AddDairySperm(DairySperm dairySperm)
        {
            if (dairySperm == null)
            {
                throw new DairySpermInvalidException();
            }

            _DairyRepo.AddDairySpermAsync(dairySperm);
        }

        public List<DairySperm> FilterDairySperms(DairyFilterDTO dairyFilterDTO)
        {
            var response = _DairyRepo.GetDairySpermsAsync().Result.OrderBy(x =>x.CustomOrder)
                .ThenByDescending(x=>x.IsNew)
                .ThenBy(x=>x.FM)
                .ThenBy(x=>x.LNM)
                .ThenBy(x => x.MILK)
                .ThenBy(x => x.PL)
                .ThenBy(x => x.TPI)
                .ToList();

            if (response == null)
            {
                throw new DairySpermNotFoundException();
            }

            if (dairyFilterDTO == null)
            {
                return response;
            }

            response = StringsFilter(response,dairyFilterDTO);

            if (!string.IsNullOrEmpty(dairyFilterDTO.Range))
            {

                response = StoreAndFilterByRange(response, dairyFilterDTO.Range);
            }

            response = OrderingDairySperms(response, dairyFilterDTO.IsDescending, dairyFilterDTO.OrderBy);

            if (response.Count <= 0)
            {
                throw new DairySpermFilterException();
            }

            return response;
        }

        private List<DairySperm> OrderingDairySperms(List<DairySperm> dairySperms,bool isDescending,string orderBy)
        {
            if (isDescending)
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    dairySperms = dairySperms.OrderByDescending(x => x.GetType().GetProperty(orderBy).GetValue(x)).ToList();
                }
                else
                {
                    dairySperms = dairySperms.OrderByDescending(x => x.CustomOrder).ThenBy(x => x.IsNew).ThenByDescending(x => x.FM).ThenByDescending(x => x.LNM).ThenByDescending(x => x.MILK).ThenByDescending(x => x.PL).ThenByDescending(x => x.TPI).ToList();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(orderBy))
                {
                    dairySperms = dairySperms.OrderBy(x => x.GetType().GetProperty(orderBy).GetValue(x)).ToList();
                }
            }

            return dairySperms;
        }

        private List<DairySperm> StoreAndFilterByRange(List<DairySperm> dairySperms,string range)
        {
            var deserializedJson = JsonSerializer.Deserialize<RangeListModel>(range);


            foreach (var index in deserializedJson.Filters)
            {
                //save search info
                var rangeFilter = _mapper.Map<RangeFilter>((index, deserializedJson));
                _DairyRepo.AddRangeFilterAsync(rangeFilter);

                if (index.MinValue != 0 && index.MaxValue != 0)
                {
                    dairySperms = dairySperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    >= index.MinValue && (double)x.GetType().GetProperty(index.Index).GetValue(x) <= index.MaxValue).ToList();
                }

                else if (index.MinValue != 0)
                {
                    dairySperms = dairySperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    >= index.MinValue).ToList();
                }

                else if (index.MaxValue != 0)
                {
                    dairySperms = dairySperms.Where(x => (double)x.GetType().GetProperty(index.Index).GetValue(x)
                    <= index.MaxValue).ToList();
                }
            }

            return dairySperms;
        }

        private List<DairySperm> StringsFilter(List<DairySperm> dairySperms,DairyFilterDTO dairyFilterDTO)
        {
            if (!string.IsNullOrEmpty(dairyFilterDTO.Id))
            {
                dairySperms = dairySperms.Where(x => x.Id == dairyFilterDTO.Id).ToList();
            }

            if (!string.IsNullOrEmpty(dairyFilterDTO.RegNo))
            {
                dairySperms = dairySperms.Where(x => x.RegNo == dairyFilterDTO.RegNo).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.NAAB_CODE))
            {
                dairySperms = dairySperms.Where(x => x.NAAB_CODE == dairyFilterDTO.NAAB_CODE).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.NAME))
            {
                dairySperms = dairySperms.Where(x => x.NAME == dairyFilterDTO.NAME).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.SIRE))
            {
                dairySperms = dairySperms.Where(x => x.SIRE == dairyFilterDTO.SIRE).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.MGS))
            {
                dairySperms = dairySperms.Where(x => x.MGS == dairyFilterDTO.MGS).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.Breed))
            {
                dairySperms = dairySperms.Where(x => x.Breed == dairyFilterDTO.Breed).ToList();
            }

            return dairySperms;
        }

        public DairySperm FindSperm(string id)
        {
            var result = _DairyRepo.FindDairySpermAsync(id).Result;

            if (result == null)
            {
                throw new DairySpermNotFoundException(id);
            }

            return result;
        }

        public void UpdateDairySperm(DairySperm dairySperm)
        {
            if (dairySperm == null)
            {
                throw new DairySpermInvalidException();
            }

            _DairyRepo.UpdateDairySpermAsync(dairySperm);
        }

        public void DeleteSperm(string id)
        {
            _DairyRepo.DeleteDairySpermAsync(id);
        }

        public void DeleteAllSperms()
        {
            _DairyRepo.DeleteAllDairySpermsAsync();
        }
    }
}
