using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpermCatalog.API.Services.FiltersServices
{
    public class FilterServices : IFilterServices
    {
        private readonly IBeefSpermServices _beefSpermServices;
        private readonly IDairyServices _dairyServices;
        public FilterServices(IBeefSpermServices beefSpermServices,IDairyServices dairyServices)
        {
            _beefSpermServices = beefSpermServices;
            _dairyServices = dairyServices;
        }

        
        public List<BeefResponseDTO> FilterBeefSperms(BeefFilterDTO beefFilterDTO)
        {
            var response = _beefSpermServices.BeefSpermListResponse();
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


            if (beefFilterDTO.isDescending != false)
            {
                response = response.OrderByDescending(x=>x.Id).ToList();
            }

            return response;
        }

        public List<DairyResponseDTO> FilterDairySperms(DairyFilterDTO dairyFilterDTO)
        {
            var response = _dairyServices.DairySpermListResponse();
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


            if (dairyFilterDTO.isDescending != false)
            {
                response = response.OrderByDescending(x => x.Id).ToList();
            }

            return response;
        }


    }
}
