namespace GhostServiceBuster.IntegrationTests.Testees;

public static class TestMinimalApiHandler
{
    public static string Handle(IService4 service) => service.GetType().Name;
}
