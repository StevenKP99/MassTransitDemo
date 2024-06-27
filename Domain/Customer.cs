using Domain.Events;

namespace Domain;

public class Customer : AuditableEntity
{
    public Guid SystemId { get; private set; } = Guid.NewGuid();

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email {  get; private set; } = string.Empty;

    public override void SetCreatedDate()
    {
        var createdEvent = new CustomerCreated(Guid.NewGuid());
        RaiseEvent(createdEvent);

        base.SetCreatedDate();
    }

    public Customer UpdateFirstName(string firstName)
    {
        this.FirstName = firstName;

        return this;
    }

    public Customer UpdateLastName(string lastName)
    {
        this.LastName = lastName;

        return this;
    }

    public Customer UpdateEmail(string email)
    {
        this.Email = email;

        return this;
    }
}
