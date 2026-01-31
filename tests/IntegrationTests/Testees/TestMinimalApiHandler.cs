using Microsoft.AspNetCore.Mvc;

namespace GhostServiceBuster.IntegrationTests.Testees;

public static class TestMinimalApiHandler
{
    public static string Handle([FromServices] IService4 service) => service.GetType().Name;
}
