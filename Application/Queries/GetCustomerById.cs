using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries;

public record GetCustomerByIdRequest(int id) : IRequest<Customer>;
internal class GetCustomerById : IRequestHandler<GetCustomerByIdRequest, Customer>
{
    private readonly CustomerDbContext _context;

    public GetCustomerById(CustomerDbContext context) => _context = context;
    public async Task<Customer> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.id);

        if (customer == null) 
        {
            throw new ApplicationException($"Customer not found for id {request.id}"); 
        }
       
        return customer;
    }
}
