using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OagengTechnicalTest.Data;

namespace OagengTechnicalTest.Controllers
{
    public class UploadController : Controller
    {
        private readonly TransactionsService _service;
        public UploadController(TransactionsService service)
        {
            _service = service;
        }

        [HttpPost("upload/upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                _service.uploadFile(file);
                return StatusCode(200);
            }
            catch
            {
                throw;
            }
          
        }
    }
}
