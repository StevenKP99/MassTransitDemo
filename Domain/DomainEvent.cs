using MediatR;

namespace Domain;

public interface IDomainEvent : INotification;
public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime EventDate { get; set; } = DateTime.UtcNow;
}
