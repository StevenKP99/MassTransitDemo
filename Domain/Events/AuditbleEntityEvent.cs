namespace Domain.Events;

public sealed record AuditbleEntityEvent( string Action, string[]? Details, DateTime UpdateDate) : IDomainEvent;