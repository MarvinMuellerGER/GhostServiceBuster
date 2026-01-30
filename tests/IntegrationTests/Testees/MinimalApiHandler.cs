namespace GhostServiceBuster.IntegrationTests.Testees;

public static class MinimalApiHandler
{
    public static string Handle(IService4 service) => service.GetType().Name;
}
