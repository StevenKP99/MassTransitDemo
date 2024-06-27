using Domain;
using Infratructure.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace Infrastructure.Interceptors;

public class PublishDomainEventsIntercepter : SaveChangesInterceptor
{
    private readonly ILogger<PublishDomainEventsIntercepter> _logger;
    private readonly IBus _bus;

    public PublishDomainEventsIntercepter(ILogger<PublishDomainEventsIntercepter> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is not null)
        {
            PublishDomainEvents(dbContext);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);

    }

    private void PublishDomainEvents(DbContext context)
    {
        var outboxMessages = context.ChangeTracker
            .Entries<Entity>()
            .Select(e => e.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetEventsReadOnly();

                entity.ClearEvents();

                return domainEvents;
            })
            .Select
            (
                domainEvent => new CustomerOutboxMessage()
                {
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
                    OccurredOntUtc = DateTime.UtcNow,
                }
            )
            .ToList();

        foreach(var outBoxMessage in outboxMessages)
        {
            _bus.Publish(outBoxMessage);
        }
    }
}
