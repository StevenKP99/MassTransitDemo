using Domain.Events;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Notifications;

internal class CustomerCreatedNotificationHandler : INotificationHandler<CustomerCreated>
{
    private readonly ISender _sender;
    private readonly CustomerDbContext _context;

    public CustomerCreatedNotificationHandler(CustomerDbContext context, ISender sender) 
    {
        _sender = sender; 
        _context = context;
    }
    public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
    {
        //await Task.Delay(TimeSpan.FromSeconds(20));
        //Console.WriteLine("CUSTOMER EMAIL NOTIFICATION");

        ///////Audit Log
        var customer = _context.Customers.Where( customer => customer.SystemId == notification.CustomerSystemId ).FirstOrDefault();

        var auditLogRequest = new AuditEntryCreatedRequest(customer.Key, customer.DataSource, "Customer Created", createdBy, createdDateTime);
        var status = await _sender.Send(auditLogRequest);
 
        //////Send Email
        string sender = "";
        string reciepenants = customer.CreatedBy
        string bodyMessage = "TEMPLATE OBJECT";

        var emailRequest = SendEmailRequest(sender, reciepenants, bodyMessage);

        _sender.Send(SendEmailRequest);
    }
}