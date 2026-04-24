
using ExamSystem.Web.Options;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Reflection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiOptions>(configuration.GetSection(ApiOptions.SectionName));

        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(HttpRequestMessage request, IServiceProvider sp)
        {
            var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    opts.RetryCount,
                    attempt => TimeSpan.FromSeconds(Math.Pow(opts.RetryDelaySeconds, attempt)),
                    onRetry: (outcome, delay, attempt, context) =>
                    {
                        var logger = sp.GetRequiredService<ILogger<HttpClient>>();
                        logger.LogWarning("Retry {Attempt} — {Delay}s — {Reason}",
                            attempt, delay.TotalSeconds, outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString());
                    });
        }

        void ConfigureClient(IServiceProvider sp, HttpClient client)
        {
            var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        }

        services.AddHttpClient<CourseApiClient>(ConfigureClient).AddPolicyHandler((sp, request) => GetRetryPolicy(request, sp));
        services.AddHttpClient<StudentApiClient>(ConfigureClient).AddPolicyHandler((sp, request) => GetRetryPolicy(request, sp));
        services.AddHttpClient<ExamApiClient>(ConfigureClient).AddPolicyHandler((sp, request) => GetRetryPolicy(request, sp));

        return services;
    }

    public static IServiceCollection AddWebMvc(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
            //options.Filters.Add<ValidationFilter>();
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.DateFormatString = "dd-MM-yyyy";
        });

        return services;
    }

    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        //services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}