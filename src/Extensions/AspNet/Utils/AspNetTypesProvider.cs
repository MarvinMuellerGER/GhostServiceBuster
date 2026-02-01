using Microsoft.AspNetCore.Http;

namespace GhostServiceBuster.AspNet.Utils;

internal static class AspNetTypesProvider
{
    public static IEnumerable<Type> EndpointFilters =>
        field ??= AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IEndpointFilter)));
}