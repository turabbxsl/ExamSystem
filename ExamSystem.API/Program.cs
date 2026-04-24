

using ExamSystem.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// 1. Service Registration
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices();

var allowedOrigins = builder.Configuration["CorsSettings:AllowedOrigins"]?.Split(',');
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseGlobalExceptionHandler();

app.UseCors("AllowWebApp");

app.UseApiFeatures();

app.MapExamEndpoints();
app.MapCourseEndpoints();
app.MapStudentEndpoints();

app.MapGet("/", () => "API IS WORKING!");
app.MapGet("/health", () => "OK");

app.Services.ApplyMigrations();

app.Run();