using Application.Queries;
using Domain;
using MediatR;

namespace Customers.SDK.Queries;

public class CustomerQueries
{
    private readonly ISender _sender;

    public CustomerQueries(ISender sender) => _sender = sender;

    public async Task<Customer> GetCustomerById(GetCustomerByIdRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(request);

        return response;
    }

    public async Task<List<Customer>> GetAllCustomers(GetAllCustomersRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(request); 
        
        return response;
    }
}
