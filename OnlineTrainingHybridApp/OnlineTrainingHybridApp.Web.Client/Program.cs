using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineTrainingHybridApp.Shared.Services;
using OnlineTrainingHybridApp.Web.Client.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the OnlineTrainingHybridApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// ✅ Tambahkan ini: HttpClient untuk komunikasi API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
