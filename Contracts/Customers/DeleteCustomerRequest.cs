using Domain;
using MediatR;

namespace Contracts.Customers;

public record DeleteCustomerRequest(int id) : IRequest<bool>;