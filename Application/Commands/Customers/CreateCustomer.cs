using Contracts.Customers;
using Domain;
using Domain.Events;
using Infrastructure.Data;
using MassTransit;
using MediatR;

namespace Application.Commands.Customers;

internal class CreateCustomer : IRequestHandler<CreateCustomerRequest, Customer>
{
    private readonly CustomerDbContext _context;
    private readonly IPublishEndpoint _publish;

    public CreateCustomer(CustomerDbContext customerDbContext, IPublishEndpoint publish)
    {
        _context = customerDbContext; 
        _publish = publish;
    }
    public async Task<Customer> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var newCustomer = new Customer()
            .UpdateFirstName(request.firstName)
            .UpdateLastName(request.lastName)
            .UpdateEmail(request.email);
            
        newCustomer.SetCreatedDate();
        await _context.AddAsync(newCustomer);

        var createdEvent = new CustomerCreated(newCustomer.SystemId);
        await _publish.Publish(createdEvent);

        await _context.SaveChangesAsync();

        return newCustomer;
    }
}
