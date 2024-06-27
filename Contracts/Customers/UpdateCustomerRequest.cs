using MediatR;

namespace Contracts.Customers;


public record UpdateCustomerRequest(int id, string firstName, string lastName, string email) : IRequest<bool>;
