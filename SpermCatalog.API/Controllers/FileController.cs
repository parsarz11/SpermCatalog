using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpermCatalog.API.Contracts;

namespace SpermCatalog.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileServices _fileServices;

        public FileController(IFileServices fileServices)
        {
            _fileServices = fileServices;
        }

        [HttpPost]
        public IActionResult AddDairyFile(IFormFile file)
        {
            _fileServices.DairyCsvReader(file);
            return Ok();
        }


        [HttpPost]
        public IActionResult AddBeefFile(IFormFile file)
        {
            _fileServices.BeefCsvReader(file);
            return Ok();
        }
    }
}
