namespace SpermCatalog.API.models.DTOs.Filters
{
    public class DairyFilterDTO
    {
        public string? Id { get; set; }
        public string? RegNo { get; set; }
        public string? NAAB_CODE { get; set; }
        public string? NAME { get; set; }
        public string? SIRE { get; set; }
        public string? MGS { get; set; }
        public string? Breed { get; set; }

        public bool IsDescending { get; set; } = false;
        public string? OrderBy { get; set; }

        public string? Range { get; set; }
    }
}
