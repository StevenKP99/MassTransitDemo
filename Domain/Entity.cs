using System.Collections.ObjectModel;

namespace Domain;

public abstract class Entity
{
    public int Id { get; set; }

    private List<IDomainEvent> events = new List<IDomainEvent>();

    public void RaiseEvent(IDomainEvent e) => events.Add(e);

    public IReadOnlyCollection<IDomainEvent> GetEventsReadOnly() => events.ToList();

    public void ClearEvents() => events.Clear();
}
