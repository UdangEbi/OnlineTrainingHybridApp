using Microsoft.Extensions.Logging;
using OnlineTrainingHybridApp.Services;
using OnlineTrainingHybridApp.Shared;
using OnlineTrainingHybridApp.Shared.Services;

namespace OnlineTrainingHybridApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Blazor WebView untuk MAUI Hybrid
            builder.Services.AddMauiBlazorWebView();

            // Service device-specific yang sudah ada
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            // ✅ HttpClient untuk semua komponen Blazor & service (Shared)
            //    @inject HttpClient Http akan memakai instance ini
            builder.Services.AddSingleton(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7093/") // SESUAI backend kamu
                });

            // ✅ Service kamera + upload (MediaPicker + HttpClient)
            builder.Services.AddSingleton<IPhotoService, PhotoService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
