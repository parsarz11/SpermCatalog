using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
using SpermCatalog.API.models;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static SpermCatalog.API.models.TimeSelectionModel;

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

        public async Task AddRangeDairySpermsAsync(List<DairySperm> spermList)
        {
            if (spermList == null || spermList.Count <= 0)
            {
                throw new DairySpermInvalidException();
            }

            await _DairyRepo.AddRangeDairySpermsAsync(spermList);
        }

        public async Task AddDairySpermAsync(DairySperm dairySperm)
        {
            if (dairySperm == null)
            {
                throw new DairySpermInvalidException();
            }

            await _DairyRepo.AddDairySpermAsync(dairySperm);
        }

        public async Task<List<DairySperm>> FilterDairySpermsAsync(DairyFilterDTO dairyFilterDTO)
        {
            var response = await _DairyRepo.GetDairySpermsAsync();

            response = response.DairyDefaultOrder();

            if (response.Count <= 0 || response is null)
            {
                //throw new DairySpermNotFoundException();
                return response;
            }

            if (dairyFilterDTO == null)
            {
                return response;
            }

            response = await Task.Run(() => StringsFilter(response, dairyFilterDTO) );

            if (!string.IsNullOrEmpty(dairyFilterDTO.Range))
            {

                response = await Task.Run(() => StoreAndFilterByRange(response, dairyFilterDTO.Range));
            }

            response = await Task.Run(() => OrderingDairySperms(response, dairyFilterDTO.IsDescending , dairyFilterDTO.OrderBy));

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
                    dairySperms = dairySperms.DairyDefaultDescendingOrder();
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

        public async Task<DairySperm> FindSpermAsync(string id)
        {
            var result = await _DairyRepo.FindDairySpermAsync(id);

            //if (result == null)
            //{
            //    //throw new DairySpermNotFoundException(id);
            //}

            return result;
        }

        public async Task UpdateDairySpermAsync(DairySperm dairySperm)
        {
            if (dairySperm == null || string.IsNullOrEmpty(dairySperm.Id))
            {
                throw new DairySpermInvalidException();
            }

             await _DairyRepo.UpdateDairySpermAsync(dairySperm);
        }

        public async Task DeleteSpermAsync(string id)
        {
            await _DairyRepo.DeleteDairySpermAsync(id);
        }

        public async Task DeleteAllSpermsAsync()
        {
            await _DairyRepo.DeleteAllDairySpermsAsync();
        }

        public async Task<List<RangeFilter>> GetRangeFiltersAsync(string? category = default)
        {
            var rangeFilters = await _DairyRepo.GetRangeFiltersAsync();

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
                if (groupedRangeFilter.Any(x=>x.MinValue > -9999999))
                {
                    var minValueAvg = groupedRangeFilter.Average(x => x.MinValue);

                    avgRangeFilterModel.MinValueAvg = Math.Round(minValueAvg, 3);
                }

                //get max value average
                if (groupedRangeFilter.Any(x=>x.MaxValue < 9999999))
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
                    var totalCount = groupedRangeFilter.Select(x=>x.MaxValue).Count() + groupedRangeFilter.Select(x => x.MinValue).Count();

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
