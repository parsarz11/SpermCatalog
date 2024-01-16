using CsvHelper;
using CsvHelper.Configuration;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs.csvDTOs;
using System.Globalization;

namespace SpermCatalog.API.Services.FileServices
{
    public class FileServices : IFileServices
    {
        private readonly IDairyServices _dairyServices;
        private readonly IBeefSpermServices _beefSpermServices;
        public FileServices(IDairyServices dairyServices, IBeefSpermServices beefSpermServices)
        {
            _dairyServices = dairyServices;
            _beefSpermServices = beefSpermServices;
        }


        public void DairyCsvReader(IFormFile file)
        {
            var diarySpermList = new List<DairySpermCsvDTO>();


            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                
            
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    
                    using (var csv = new CsvReader(stream,CultureInfo.InvariantCulture))
                    {
                        diarySpermList = csv.GetRecords<DairySpermCsvDTO>().ToList();
                    }
                }
            }

            _dairyServices.AddDairySperms(diarySpermList);

        }


        public void BeefCsvReader(IFormFile file)
        {
            var beefSpermList = new List<BeefSpermCsvDTO>();


            using (var stream = new StreamReader(file.OpenReadStream()))
            {

                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    beefSpermList = csv.GetRecords<BeefSpermCsvDTO>().ToList();
                }
            }


            _beefSpermServices.AddBeefSperms(beefSpermList);
        }


    }
}
