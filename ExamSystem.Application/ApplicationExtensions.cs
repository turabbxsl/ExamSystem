using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}