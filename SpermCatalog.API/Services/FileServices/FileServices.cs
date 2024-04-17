using CsvHelper;
using CsvHelper.Configuration;
using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Exceptions;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.DataAccess.Entities;
using System.Globalization;

namespace SpermCatalog.API.Services.FileServices
{
    public class FileServices : IFileServices
    {
        private readonly IDairyServices _dairyServices;
        private readonly IBeefSpermServices _beefSpermServices;
        private readonly IMapper _mapper;
        public FileServices(IDairyServices dairyServices, IBeefSpermServices beefSpermServices, IMapper mapper)
        {
            _dairyServices = dairyServices;
            _beefSpermServices = beefSpermServices;
            _mapper = mapper;
        }


        public void DairyCsvReader(IFormFile file)
        {
            var dairySpermList = new List<AddDairySpermDTO>();


            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                
            
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    
                    using (var csv = new CsvReader(stream,CultureInfo.InvariantCulture))
                    {
                        dairySpermList = csv.GetRecords<AddDairySpermDTO>().ToList();
                    }
                }
            }
            if (dairySpermList.Count <=0)
            {
                throw new FileException();
            }
            var spermsList = _mapper.Map<List<DairySperm>>(dairySpermList);
            _dairyServices.AddRangeDairySperms(spermsList);

        }


        public void BeefCsvReader(IFormFile file)
        {
            var beefSpermList = new List<AddBeefSpermDTO>();


            using (var stream = new StreamReader(file.OpenReadStream()))
            {

                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    beefSpermList = csv.GetRecords<AddBeefSpermDTO>().ToList();
                }
            }
            if (beefSpermList.Count <= 0)
            {
                throw new FileException();
            }
            var spermsList = _mapper.Map<List<BeefSperm>>(beefSpermList);
            _beefSpermServices.AddRangeBeefSperms(spermsList);
        }


    }
}
