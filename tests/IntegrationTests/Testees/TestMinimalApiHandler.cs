using Microsoft.AspNetCore.Mvc;

namespace GhostServiceBuster.IntegrationTests.Testees;

public static class TestMinimalApiHandler
{
    public static string Handle([FromServices] IServiceInjectedIntoMinimalApiHandler service) => service.GetType().Name;
}