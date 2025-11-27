using Microsoft.AspNetCore.Mvc;
using OnlineTrainingHybridApp.Backend.PublicClasses;

namespace OnlineTrainingHybridApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public MainController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("uploadfile")]
        public IActionResult UploadFile(IFormFile file)
        {
            var handler = new UploadHandler(_env);
            var result = handler.Upload(file);

            if (result.StartsWith("ERROR"))
                return BadRequest(new { message = result });

            // ✅ PERBAIKAN: Gunakan URL HTTP yang dikonfirmasi dan hardcode port 5019
            // Kita harus menggunakan Request.Scheme dan Request.Host.Host untuk mencegah
            // masalah pada environment Blazor Hybrid/WebView yang tidak mengenal Host.

            // Catatan: Port HTTP Anda 5019 (dari CMD: http://localhost:5019)
            var fileUrl = $"http://localhost:5019/uploads/{result}";

            return Ok(new { url = fileUrl });
        }
    }
}