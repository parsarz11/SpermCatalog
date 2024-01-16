namespace SpermCatalog.API.models.DTOs.Filters
{
    public class BeefFilterDTO
    {
        public int? Id { get; set; }
        public string? RegNo { get; set; }
        public string? BREED { get; set; }
        public string? NAME { get; set; }
        public string? SIRE { get; set; }
        public string? MGS { get; set; }

        public bool isDescending { get; set; } = false;
        public string? Range { get; set; }
    }
}
