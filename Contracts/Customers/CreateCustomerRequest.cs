using Domain;
using MediatR;

namespace Contracts.Customers;

public record CreateCustomerRequest(string firstName, string lastName, string email) : IRequest<Customer>;
