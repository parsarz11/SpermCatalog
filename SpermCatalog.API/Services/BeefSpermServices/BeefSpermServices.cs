using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
using SpermCatalog.API.models;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using static SpermCatalog.API.models.TimeSelectionModel;

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


        public async Task AddRangeBeefSpermsAsync(List<BeefSperm> spermList)
        {
            if (spermList == null || spermList.Count <= 0)
            {
                throw new BeefSpermInvalidDataException();
            }

            await _beefRepo.AddRangeBeefSpermsAsync(spermList);
        }

        public async Task AddBeefSpermAsync(BeefSperm beefSperm)
        {
            if (beefSperm == null)
            {
                throw new BeefSpermInvalidDataException();
            }

            await _beefRepo.AddBeefSpermAsync(beefSperm);
        }




        public async Task<List<BeefSperm>> FilterBeefSpermsAsync(BeefFilterDTO beefFilterDTO)
        {
            var response = await _beefRepo.GetBeefSpermsAsync();
            response = response.BeefDefaultOrder();

            if (response.Count <= 0 || response is null)
            {
                //throw new DairySpermNotFoundException();
                return response;
            }

            if (beefFilterDTO == null)
            {
                return response;
            }

            response = await Task.Run(() => StringsFilter(response, beefFilterDTO));


            if (!string.IsNullOrEmpty(beefFilterDTO.Range))
            {
                response = await Task.Run(() => StoreAndFilterByRange(response, beefFilterDTO.Range));
            }


            response = await Task.Run(() => OrderingBeefSperms(response, beefFilterDTO.IsDescending, beefFilterDTO.OrderBy));

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
                    beefSperms = beefSperms.BeefDefaultOrder();
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


        public async Task<BeefSperm> FindSpermAsync(string id)
        {
            var result = await _beefRepo.FindBeefSpermAsync(id);

            //if (result == null)
            //{
            //    //throw new DairySpermNotFoundException(id);
            //}

            return result;
        }
        
        public async Task UpdateBeefSpermAsync(BeefSperm beefSperm)
        {
            if (beefSperm == null || string.IsNullOrEmpty(beefSperm.Id))
            {
                throw new BeefSpermInvalidDataException();
            }

           await _beefRepo.UpdateBeefSpermAsync(beefSperm);
        }

        public async Task DeleteSpermAsync(string id)
        {
           await _beefRepo.DeleteBeefSpermAsync(id);
        }

        public async Task DeleteAllSpermsAsync()
        {
           await _beefRepo.DeleteAllBeefSpermsAsync();
        }

        public async Task<List<RangeFilter>> GetRangeFiltersAsync(string? category = default)
        {
            var rangeFilters = await _beefRepo.GetRangeFiltersAsync();

            if (category != null)
            {
                rangeFilters = rangeFilters.Where(x => x.Category == category).ToList();
            }

            return rangeFilters;
        }

        public async Task<List<RangeFilterCountModel>> CalculateRangeFilterSearchCountAsync(TimeSelectionEnum timeSelection,string category)
        {
            var rangeFilters = await GetRangeFiltersAsync(category);

            //filter by date
            rangeFilters = await Task.Run(() => FilterByDate(timeSelection, rangeFilters));

            List<RangeFilterCountModel> rangeFilterCountModelList = new List<RangeFilterCountModel>();

            //group range filter records by index name
            var groupedIndexs = rangeFilters.GroupBy(x => x.Index).ToList();

            //count range filter records
            foreach (var groupedIndex in groupedIndexs)
            {
                //get search count of index
                var indexSearchCount = groupedIndex.Count();

                //set search count to index
                RangeFilterCountModel rangeFilterCountModel = new RangeFilterCountModel
                {
                    Index = groupedIndex.Key,
                    Count = indexSearchCount
                };

                rangeFilterCountModelList.Add(rangeFilterCountModel);
            }

            return rangeFilterCountModelList;
        }

        public async Task<List<AvgRangeFilterModel>> CalculateRangeFilterAvgAsync(TimeSelectionEnum timeSelection,string category)
        {
            //get range filters
            var rangeFilters = await GetRangeFiltersAsync(category);

            //filter by date
            rangeFilters = await Task.Run(() => FilterByDate(timeSelection, rangeFilters));

            //grouping range filters by index
            var groupedRangeFilters = rangeFilters.GroupBy(x => x.Index).ToList();

            //create an instace of list of avgRangeFilterModel 
            List<AvgRangeFilterModel> avgRangeFilterList = new List<AvgRangeFilterModel>();

            foreach (var groupedRangeFilter in groupedRangeFilters)
            {
                //set index name
                var avgRangeFilterModel = new AvgRangeFilterModel
                {
                    Index = groupedRangeFilter.Key,
                };

                //get min value average
                if (groupedRangeFilter.Any(x => x.MinValue > -9999999))
                {
                    var minValueAvg = groupedRangeFilter.Average(x => x.MinValue);

                    avgRangeFilterModel.MinValueAvg = Math.Round(minValueAvg, 3);
                }

                //get max value average
                if (groupedRangeFilter.Any(x => x.MaxValue < 9999999))
                {
                    var maxValueAvg = groupedRangeFilter.Average(x => x.MaxValue);

                    avgRangeFilterModel.MaxValueAvg = Math.Round(maxValueAvg, 3);
                }

                //get total average of all min and max values
                if (groupedRangeFilter.Any(x => x.MaxValue < 9999999) && groupedRangeFilter.Any(x => x.MinValue > -9999999))
                {
                    //get total sum for average
                    var totalSum = groupedRangeFilter.Sum(x => x.MinValue) + groupedRangeFilter.Sum(x => x.MaxValue);

                    //get total count for average
                    var totalCount = groupedRangeFilter.Select(x => x.MaxValue).Count() + groupedRangeFilter.Select(x => x.MinValue).Count();

                    //calculate the average
                    var totalAverage = totalSum / totalCount;

                    //set total average to model
                    avgRangeFilterModel.TotalAverage = Math.Round(totalAverage, 3);
                }

                //add model to list of avgRabgeFilterModel
                avgRangeFilterList.Add(avgRangeFilterModel);
            }
            return avgRangeFilterList;
        }
        private List<RangeFilter> FilterByDate(TimeSelectionEnum timeSelection, List<RangeFilter> rangeFilters)
        {
            // get todays date
            var todaydate = DateOnly.FromDateTime(DateTime.Now);

            //filter range filters by time
            if (timeSelection == TimeSelectionEnum.Today)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate == todaydate).ToList();
            }
            if (timeSelection == TimeSelectionEnum.SevenDays)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate >= todaydate.AddDays(-7)).ToList();
            }
            if (timeSelection == TimeSelectionEnum.ThisMonth)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate.Month == todaydate.Month && x.FilterDate.Year == todaydate.Year).ToList();
            }
            if (timeSelection == TimeSelectionEnum.LastMonth)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate.Month == todaydate.AddMonths(-1).Month && x.FilterDate.Year == todaydate.Year).ToList();
            }
            if (timeSelection == TimeSelectionEnum.ThisYear)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate.Year == todaydate.Year).ToList();
            }
            if (timeSelection == TimeSelectionEnum.LastYear)
            {
                rangeFilters = rangeFilters.Where(x => x.FilterDate.Year == todaydate.AddYears(-1).Year).ToList();
            }

            return rangeFilters;
        }
    }
}
