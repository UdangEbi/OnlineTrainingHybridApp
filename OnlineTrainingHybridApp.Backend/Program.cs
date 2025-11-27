using OnlineTrainingHybridApp.Backend.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddSqlite<OnlineTrainingContext>("Data Source=OnlineTraining.db");

// Dependency Injection
builder.Services.AddScoped<ICourses, CoursesData>();
builder.Services.AddScoped<ITrainers, TrainersData>();

// CORS (supaya Blazor Hybrid bisa akses API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Batasi ukuran upload (10 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
});

// ✅ Swagger configuration
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OnlineTrainingContext>();
    SeedData.Initialize(db);
}

app.Run();
