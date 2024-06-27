using Infrastructure.Data;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddCustomersDbContext( this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<AuditableItemUpdatedIntercepter>();
        services.AddSingleton<PublishDomainEventsIntercepter>();

        services.AddDbContext<CustomerDbContext>((sp, cfg) =>
        {
            List<IInterceptor> interceptors = new List<IInterceptor>()
            {
                sp.GetRequiredService<AuditableItemUpdatedIntercepter>(),
                sp.GetRequiredService<PublishDomainEventsIntercepter>()
            };

            cfg.UseSqlServer(connectionString);
            cfg.AddInterceptors(interceptors);
        });

        return services;
    }
}
