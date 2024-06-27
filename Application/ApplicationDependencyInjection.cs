using Infrastructure.Data;
using MassTransit;
using MassTransit.Internals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddMassTransit( x => {

            x.AddConsumers( typeof(InfrastructureDependencyInjection).Assembly );
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            x.AddEntityFrameworkOutbox<CustomerDbContext>(options =>
            {
                options.UseSqlServer();
                options.UseBusOutbox();
                options.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
            });

            x.AddConfigureEndpointsCallback((context, name, cfg) =>
            {
                cfg.UseEntityFrameworkOutbox<CustomerDbContext>(context);
            });
        });

        return services;
    }
}
