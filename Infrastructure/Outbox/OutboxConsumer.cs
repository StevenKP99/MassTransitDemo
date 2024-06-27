using Domain;
using Infratructure.Outbox;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using MassTransit.Mediator;

namespace Application;

public class OutboxConsumer : IConsumer<CustomerOutboxMessage>
{
    private readonly ILogger<OutboxConsumer> _logger;
    private readonly IPublisher _publisher;

    public OutboxConsumer(ILogger<OutboxConsumer> logger, IPublisher mediator)
    {
        _logger = logger;
        _publisher = mediator;
    }

    public async Task Consume(ConsumeContext<CustomerOutboxMessage> context)
    {
        var message = context.Message;



        if (message is not null)
        {

            _logger.LogInformation($"Write Outbox {message.Id} {message.Content} {message.OccurredOntUtc}");
            IDomainEvent? typedMessage = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

            if (typedMessage != null) 
            {
                _logger.LogInformation($"Message: {typedMessage}");

                await _publisher.Publish(typedMessage);
            }
            else
            {
                //Log error
            }
            
        }
        else
        {
            //Log error handling
        }

    }
}
