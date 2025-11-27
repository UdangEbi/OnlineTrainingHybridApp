using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrainingHybridApp.Shared
{
    public interface IPhotoService
    {
        /// <summary>
        /// Buka kamera (MediaPicker), ambil foto, upload ke API,
        /// dan mengembalikan URL file yang dikirim backend.
        /// </summary>
        /// <param name="uploadUrl">URL endpoint upload, misal: https://localhost:7093/api/Main/uploadfile</param>
        Task<string?> CaptureAndUploadPhotoAsync(string uploadUrl);
    }

    public class CapturedPhoto
    {
        public Stream Stream { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
    }
}
