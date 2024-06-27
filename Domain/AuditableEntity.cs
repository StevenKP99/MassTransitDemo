namespace Domain;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }

    public virtual void SetCreatedDate()
    {
        this.CreatedDateTime = DateTime.UtcNow;
        this.UpdatedDateTime = CreatedDateTime;
    }

    public virtual void SetUdpateDateTime()
    {
        this.UpdatedDateTime = DateTime.UtcNow;
    }
}
