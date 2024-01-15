namespace SpermCatalog.API.models.DTOs
{
    public class BeefSpermCsvDTO
    {
        public string RegNo { get; set; }
        public string BREED { get; set; }
        public string NAME { get; set; }

        public double SCE { get; set; }
        public double CR { get; set; }
        public double DM { get; set; }
        public double PCAR { get; set; }
        public double RDT { get; set; }
        public double CONF { get; set; }
        public double COUL { get; set; }
        public double GRAS { get; set; }
        public double IAB { get; set; }
        public double ICRC { get; set; }

        public string SIRE { get; set; }
        public string MGS { get; set; }
        public int Price { get; set; }
    }
}
