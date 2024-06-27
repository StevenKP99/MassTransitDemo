using Application.Notifications;
using Contracts.Customers;
using Domain;
using MediatR;

namespace Customers.SDK.Commands;

public class CustomerCommands
{
    private readonly IMediator _sender;

    public CustomerCommands(IMediator mediatR) => _sender = mediatR;

    public async Task<Customer> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response;
    }

    public async Task<bool> UpdateCustomer(UpdateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response;
    }

    public async Task<bool> DeleteCustomer(DeleteCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response;
    }
}
