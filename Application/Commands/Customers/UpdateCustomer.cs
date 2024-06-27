using Contracts.Customers;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Customers;

internal class UpdateCustomer : IRequestHandler<UpdateCustomerRequest, bool>
{
    private readonly CustomerDbContext _context;

    public UpdateCustomer(CustomerDbContext context) => _context = context;
    public async Task<bool> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.id, cancellationToken);

        if (customer == null) { return false; }

        customer.UpdateFirstName(request.firstName);
        customer.UpdateLastName(request.lastName);
        customer.UpdateEmail(request.email);
        customer.SetUdpateDateTime();

        _context.Update(customer);
        var count = await _context.SaveChangesAsync();

        return count > 0;

    }
}
