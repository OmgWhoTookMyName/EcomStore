using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstEcommerceStore.Controllers
{
    [DisableRequestSizeLimit]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment enviro)
        {
            _webHostEnvironment = enviro;
        }

        [HttpPost("upload/single")]
        public async Task<IActionResult> SingleAsync(IFormFile file)
        {
            try
            {
                await UploadFile(file);
                return StatusCode(200);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if(file !=null && file.Length > 0)
            {
                var imagePath = @"\Upload";
                var uploadPath = _webHostEnvironment.WebRootPath + imagePath;
                if(!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fullPath = Path.Combine(uploadPath, file.FileName);
                using(FileStream fileSreatm = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileSreatm);
                }
                return Ok($"File {file.Name} was uploaded successfully.");
            }
            else
            {
                return StatusCode(500);
            }
            
        }
    }
}
