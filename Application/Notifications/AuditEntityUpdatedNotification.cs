using Domain;
using Domain.Events;
using Infrastructure.Data;
using MediatR;

namespace Application.Notifications;


internal class AuditEntityUpdatedNotification : INotificationHandler<AuditbleEntityEvent>
{
    private readonly CustomerDbContext _context;

    public AuditEntityUpdatedNotification( CustomerDbContext context ) => _context = context;
    public async Task Handle(AuditbleEntityEvent notification, CancellationToken cancellationToken)
    {
        string details = "";

        if (notification.Action.Equals("Added")) details = "Created";
        else if (notification.Action.Equals("Modified") && notification.Details is not null) details = string.Join( ", ", notification.Details );
      
        var auditLog = new AuditLog()
        {
            Action = notification.Action,
            Details = details,
            UpdatedBy = notification.UpdateDate,
            CreatedBy = "spuckett"
        };

        await _context.AddAsync(auditLog);
        await _context.SaveChangesAsync();
    }
}
