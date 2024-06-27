using Domain;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public record GetAllCustomersRequest : IRequest<List<Customer>>;
internal class GetAllCustomers : IRequestHandler<GetAllCustomersRequest, List<Customer>>
{
    private readonly CustomerDbContext _context;

    public GetAllCustomers(CustomerDbContext context) => _context = context;
    public async Task<List<Customer>> Handle(GetAllCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = _context.Customers;

        return await customers.ToListAsync(cancellationToken);
    }
}
