using Microsoft.AspNetCore.Http;

namespace GhostServiceBuster.IntegrationTests.Testees;

public sealed class TestEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
        await next(context);
}