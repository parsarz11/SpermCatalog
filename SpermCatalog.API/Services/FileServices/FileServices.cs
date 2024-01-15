using CsvHelper;
using CsvHelper.Configuration;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.models.DTOs;
using System.Globalization;

namespace SpermCatalog.API.Services.FileServices
{
    public class FileServices : IFileServices
    {
        private readonly IDairyServices _dairyServices;

        public FileServices(IDairyServices dairyServices)
        {
            _dairyServices = dairyServices;
        }

        public void DairyCsvReader(IFormFile file)
        {
            var diarySpermList = new List<DairySpermCsvDTO>();


            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                    
                using (var csv = new CsvReader(stream,CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    diarySpermList = csv.GetRecords<DairySpermCsvDTO>().ToList();
                    var a = csv.GetRecord<object>();
                }
            }

            _dairyServices.AddDairySperms(diarySpermList);



            
        }
    }
}
