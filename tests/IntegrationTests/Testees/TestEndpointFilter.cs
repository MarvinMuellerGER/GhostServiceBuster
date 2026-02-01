using Microsoft.AspNetCore.Http;

namespace GhostServiceBuster.IntegrationTests.Testees;

public sealed class TestEndpointFilter(IService8 service) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        _ = service;

        return await next(context);
    }
}