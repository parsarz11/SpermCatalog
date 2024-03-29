﻿using MapsterMapper;
using Microsoft.IdentityModel.Tokens;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
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

        public void AddDairySperms(List<DairySpermCsvDTO> spermDTO)
        {
            var spermsList = _mapper.Map<List<DairySperm>>(spermDTO);
            _DairyRepo.AddDairySpermsListAsync(spermsList);
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

            if (dairyFilterDTO == null)
            {
                return response;
            }

            if (dairyFilterDTO.Id != null && dairyFilterDTO.Id != 0)
            {
                response = response.Where(x => x.Id == dairyFilterDTO.Id).ToList();
            }

            if (!string.IsNullOrEmpty(dairyFilterDTO.RegNo))
            {
                response = response.Where(x => x.RegNo == dairyFilterDTO.RegNo).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.NAAB_CODE))
            {
                response = response.Where(x => x.NAAB_CODE == dairyFilterDTO.NAAB_CODE).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.NAME))
            {
                response = response.Where(x => x.NAME == dairyFilterDTO.NAME).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.SIRE))
            {
                response = response.Where(x => x.SIRE == dairyFilterDTO.SIRE).ToList();
            }
            if (!string.IsNullOrEmpty(dairyFilterDTO.MGS))
            {
                response = response.Where(x => x.MGS == dairyFilterDTO.MGS).ToList();
            }

            if (response.Count <= 0)
            {
                throw new Exception("Data with those sperm filters not found| DairySpermsServices/FilterDairySperms");
            }


            if (!string.IsNullOrEmpty(dairyFilterDTO.Range))
            {

                var range = JsonSerializer.Deserialize<RangeListModel>(dairyFilterDTO.Range);


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

            if (dairyFilterDTO.IsDescending)
            {
                if (!string.IsNullOrEmpty(dairyFilterDTO.OrderBy))
                {
                    response = response.OrderByDescending(x => x.GetType().GetProperty(dairyFilterDTO.OrderBy).GetValue(x)).ToList();
                }
                else
                {
                    response = response.OrderByDescending(x => x.CustomOrder).ThenBy(x => x.IsNew).ThenByDescending(x => x.LNM).ThenByDescending(x => x.FM).ToList();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(dairyFilterDTO.OrderBy))
                {
                    response = response.OrderBy(x => x.GetType().GetProperty(dairyFilterDTO.OrderBy).GetValue(x)).ToList();
                }
            }
            

            return response;
        }


        public DairySperm FindSperm(int id)
        {
            return _DairyRepo.FindDairySpermAsync(id).Result;
        }

        public void UpdateDairySperm(DairySperm dairySperm)
        {
            _DairyRepo.UpdateDairySperm(dairySperm);
        }

        public void DeleteSperm(int id)
        {
            _DairyRepo.DeleteDairySperm(id);
        }

        public void DeleteAllSperms()
        {
            _DairyRepo.DeleteAllDairySpermsAsync();
        }
    }
}
