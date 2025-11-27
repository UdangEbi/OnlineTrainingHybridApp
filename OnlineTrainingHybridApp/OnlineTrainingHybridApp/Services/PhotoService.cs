using Microsoft.Maui.Media;
using OnlineTrainingHybridApp.Shared;
using OnlineTrainingHybridApp.Shared.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OnlineTrainingHybridApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _httpClient;

        public PhotoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> CaptureAndUploadPhotoAsync(string uploadUrl)
        {
            try
            {
                // 1. Cek apakah device mendukung capture (kamera)
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    await App.Current!.MainPage!.DisplayAlert(
                        "Error",
                        "Kamera tidak didukung di perangkat ini.",
                        "OK");
                    return null;
                }

                // 2. Buka UI kamera dan ambil foto
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null)
                    return null;

                // 3. Baca stream file
                using var stream = await photo.OpenReadAsync();

                // 4. Kirim ke API sebagai multipart/form-data
                using var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType =
                    new MediaTypeHeaderValue("image/jpeg");

                // "file" harus sama dengan parameter IFormFile di MainController.UploadFile
                content.Add(streamContent, "file", photo.FileName);

                // uploadUrl misalnya: https://localhost:7093/api/Main/uploadfile
                var response = await _httpClient.PostAsync(uploadUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    await App.Current!.MainPage!.DisplayAlert(
                        "Upload gagal",
                        $"{response.StatusCode}\n{err}",
                        "OK");
                    return null;
                }

                // 5. Baca JSON { url: "http://localhost:5019/uploads/xxx.png" }
                var json = await response.Content.ReadFromJsonAsync<UploadResult>();
                return json?.Url;
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert(
                    "Error",
                    $"Terjadi kesalahan saat mengambil / mengupload foto: {ex.Message}",
                    "OK");
                return null;
            }
        }

        private class UploadResult
        {
            public string? Url { get; set; }
        }
    }
}