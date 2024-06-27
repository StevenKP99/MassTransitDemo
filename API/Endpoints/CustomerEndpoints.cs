using API.Endpoints.Internal;
using Application.Queries;
using Contracts;
using Contracts.Customers;
using Customers.SDK.Commands;
using Customers.SDK.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class CustomerEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Customer";

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public static void DefineEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapPost(APIEndpoints.Customer.Create, Create)
             .Accepts<CreateCustomerRequest>(ContentType)
             .WithTags(Tag);

        builder.MapGet(APIEndpoints.Customer.Get, GetById)
            .Produces<Customer>()
            .WithName("GetCustomerById")
            .WithTags(Tag);

        builder.MapGet(APIEndpoints.Customer.GetAll, GetAll)
            .Produces<List<Customer>>()
            .WithTags(Tag);

        builder.MapPut(APIEndpoints.Customer.Update, Update)
            .Accepts<UpdateCustomerRequest>(ContentType)
            .WithTags(Tag);

        builder.MapDelete(APIEndpoints.Customer.Delete, Delete)
            .WithTags(Tag);
    }

    private static async Task<IResult> Create(CreateCustomerRequest request, CustomerCommands commands, CancellationToken cancellationToken) 
    {
        var response = await commands.CreateCustomer(request, cancellationToken);

        return Results.CreatedAtRoute<int>("GetCustomerById", new { id = response.Id }, response.Id);
    }

    private static async Task<IResult> GetAll( CustomerQueries queries, CancellationToken cancellationToken)
    {
        var request = new GetAllCustomersRequest();
        var response = await queries.GetAllCustomers(request, cancellationToken);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetById(int id, CustomerQueries queries, CancellationToken cancellationToken)
    {
        var request = new GetCustomerByIdRequest(id);

        var response = await queries.GetCustomerById(request, cancellationToken);

        return Results.Ok(response);
    }

    private static async Task<IResult> Update(int id, UpdateCustomerRequest request, CustomerCommands commands, CancellationToken cancellationToken)
    {
        if(id != request.id) 
        { 
            return Results.Problem("Error : Requested id update customer id must match");
        }

        var response = await commands.UpdateCustomer(request, cancellationToken);

        return Results.Ok(response);
    }

    private static async Task<IResult> Delete(int id, CustomerCommands commands, CancellationToken cancellationToken)
    {
        var request = new DeleteCustomerRequest(id);
        var response = await commands.DeleteCustomer(request, cancellationToken);

        return Results.Ok(response);
    }
}
