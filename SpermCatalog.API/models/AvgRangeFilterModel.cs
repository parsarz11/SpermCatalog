namespace SpermCatalog.API.models
{
    public class AvgRangeFilterModel
    {

        public string Index {  get; set; }
        public double? MaxValueAvg{ get; set; }
        public double? MinValueAvg { get; set; }
        public double? TotalAverage {  get; set; }
    }
}
