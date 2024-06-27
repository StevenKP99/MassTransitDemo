namespace Infratructure.Outbox;

public record CustomerOutboxMessage
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Content { get; set; } = string.Empty;

    public DateTime OccurredOntUtc { get; set; }

    public string? Error { get; set; }
}
