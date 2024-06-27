using Contracts.Customers;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Customers;

public class DeleteCustomer : IRequestHandler<DeleteCustomerRequest, bool>
{
    private readonly CustomerDbContext _context;

    public DeleteCustomer(CustomerDbContext context) => _context = context;
    public async Task<bool> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.id, cancellationToken);

        if (customer == null) { return false; }

        _context.Remove(customer);

        _context.SaveChanges();

        return true;
    }
}
