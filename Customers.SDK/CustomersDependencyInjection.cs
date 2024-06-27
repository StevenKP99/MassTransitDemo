using Application;
using Contracts;
using Customers.SDK.Commands;
using Customers.SDK.Queries;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomersDependencyInjection
{
    public static IServiceCollection AddCustomersApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if(connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString), "No Connection String Provided");
        }
        services.AddCustomersDbContext(connectionString);

        services.AddTransient<CustomerCommands>();
        services.AddTransient<CustomerQueries>();

        Assembly[] assemblies = new Assembly[] { typeof(IApplicationMarker).Assembly, typeof(IContractsMarker).Assembly };
        services.AddMediatR( (cfg) =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddMassTransitServices(configuration);

        return services;
    }
}
