namespace Domain.Events
{
    public sealed record CustomerCreated( Guid CustomerSystemId ) : IDomainEvent;
}
