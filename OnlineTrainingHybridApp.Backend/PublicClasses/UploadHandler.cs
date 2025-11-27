using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineTrainingHybridApp.Backend.PublicClasses
{
    public class UploadHandler
    {
        private readonly IWebHostEnvironment _env;

        public UploadHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "ERROR: No file was uploaded.";

            string extension = Path.GetExtension(file.FileName).ToLower();
            var validExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
            if (!validExtensions.Contains(extension))
                return $"ERROR: Extension '{extension}' is not valid. Valid extensions: {string.Join(", ", validExtensions)}";

            long maxSize = 5 * 1024 * 1024; // 5 MB
            if (file.Length > maxSize)
                return "ERROR: File size is too large. Maximum allowed size is 5 MB.";

            string fileName = $"{Guid.NewGuid()}{extension}";

            string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                return $"ERROR: Failed to save file. Details: {ex.Message}";
            }

            return fileName;  // ✅ ini nama file valid
        }

    }
}