using System.Reflection;
using System.Runtime.CompilerServices;

namespace API.Endpoints.Internal;

public static class EndpointExtensions
{
    public static void AddEndpoints<TMarker>( this IServiceCollection services, IConfiguration configuration)
    {
        AddEndpoints( services, typeof(TMarker), configuration );
    }

    public static void AddEndpoints( this IServiceCollection services, Type typeMarker, IConfiguration configuration)
    {
        IEnumerable<TypeInfo> endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach(var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.AddServices))!
                .Invoke(null, new object[] { services, configuration });
        }
    }

    public static void UseEndpoints<TMarker>(this IEndpointRouteBuilder builder)
    {
        UseEndpoints(builder, typeof(TMarker));
    }

    public static void UseEndpoints(this IEndpointRouteBuilder builder, Type typeMarker)
    {
        IEnumerable<TypeInfo> endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, new object[] { builder });
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        return typeMarker.Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints).IsAssignableFrom(x));
    }
}
