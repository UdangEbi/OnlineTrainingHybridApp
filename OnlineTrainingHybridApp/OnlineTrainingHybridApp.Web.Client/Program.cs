using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineTrainingHybridApp.Shared.Services;
using OnlineTrainingHybridApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the OnlineTrainingHybridApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
