using CsvHelper.Configuration.Attributes;

namespace SpermCatalog.API.models.DTOs.csvDTOs
{
    public class DairySpermCsvDTO
    {
        public string RegNo { get; set; }
        public string NAAB_CODE { get; set; }
        public string NAME { get; set; }

        public double ICC { get; set; }
        public double LNM { get; set; }
        public double FM { get; set; }
        public double MILK { get; set; }
        public double FAT { get; set; }
        public double PRO { get; set; }
        public double SCE { get; set; }
        public double PL { get; set; }
        public double DPR { get; set; }
        public double PTAT { get; set; }
        public double UDC { get; set; }
        public double FLC { get; set; }
        public double FS { get; set; }
        public double FI { get; set; }
        public double TPI { get; set; }



        public string SIRE { get; set; }
        public string MGS { get; set; }
        //[Name("قیمت(ریال)")]
        public int price { get; set; }
    }
}
//Reg No.	NAAB CODE	NAME	ICC	LNM	FM	MILK	FAT	PRO	SCE	PL	DPR	PTAT	UDC	FLC	FS	FI	TPI	SIRE	MGS	price
