using Scalar.AspNetCore;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static void UseApiFeatures(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Exam System API")
                       .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });
        }
    }


    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}