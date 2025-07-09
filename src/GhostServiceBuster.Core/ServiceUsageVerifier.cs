namespace GhostServiceBuster.Core;

public sealed class ServiceUsageVerifier : IServiceUsageVerifier
{
    public IReadOnlySet<ServiceInfo> GetUnusedServices(
        in IReadOnlySet<ServiceInfo> allServices, in IReadOnlySet<ServiceInfo> rootServices)
    {
        var injectedServices = rootServices;
        var unusedServices = allServices
            .Except(injectedServices)
            .ToHashSet();

        while (true)
        {
            injectedServices = unusedServices.GetServicesInjectedInto(injectedServices);

            if (injectedServices.Count is 0) return unusedServices;

            unusedServices = [.. unusedServices.Except(injectedServices)];
        }
    }
}

file static class ServiceInfoSetExtensions
{
    public static IReadOnlySet<ServiceInfo> GetServicesInjectedInto(
        this IReadOnlySet<ServiceInfo> allServices, in IReadOnlySet<ServiceInfo> injectedServices)
    {
        var injectedTypes = injectedServices.GetConstructorParameterTypes();

        return allServices
            .Where(s => injectedTypes.Contains(s.ServiceType))
            .ToHashSet();
    }

    private static IReadOnlySet<Type> GetConstructorParameterTypes(this IReadOnlySet<ServiceInfo> services)
    {
        var serviceConstructors = services.SelectMany(s => s.ImplementationType.GetConstructors());
        var serviceConstructorParameters = serviceConstructors.SelectMany(c => c.GetParameters());
        var serviceConstructorParameterTypes = serviceConstructorParameters.Select(p => p.ParameterType);

        return serviceConstructorParameterTypes.ToHashSet();
    }
}