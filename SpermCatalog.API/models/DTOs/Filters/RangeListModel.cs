namespace SpermCatalog.API.models.DTOs.Filters
{
    public class RangeListModel
    {
        public string Category { get; set; }
        public string? UserId { get; set; }
        public string? HerdId { get; set; }
        public string? HerdName { get; set; }
        public List<RangeModel> Filters { get; set; }
    }
}
