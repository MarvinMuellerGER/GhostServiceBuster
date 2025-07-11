using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Core;

internal sealed class CoreServiceUsageVerifier : ICoreServiceUsageVerifier
{
    public ServiceInfoSet GetUnusedServices(in ServiceInfoSet allServices, in ServiceInfoSet rootServices)
    {
        var injectedServices = rootServices;
        var unusedServices = allServices.Except(injectedServices);

        while (true)
        {
            injectedServices = unusedServices.GetServicesInjectedInto(injectedServices);

            if (injectedServices.Count is 0)
                return unusedServices;

            unusedServices = [.. unusedServices.Except(injectedServices)];
        }
    }
}

file static class ServiceInfoSetExtensions
{
    public static ServiceInfoSet GetServicesInjectedInto(
        this ServiceInfoSet allServices, in ServiceInfoSet injectedServices)
    {
        var injectedTypes = injectedServices.GetConstructorParameterTypes();

        return allServices.Where(serviceInfo =>
            serviceInfo.ServiceType.ConsiderGenericType(serviceType =>
                injectedTypes.Any(injectedType => injectedType.ConsiderGenericType(i => i == serviceType))));
    }

    private static IEnumerable<Type> GetConstructorParameterTypes(this ServiceInfoSet services)
    {
        var serviceConstructors = services.SelectMany(s => s.ImplementationType.GetConstructors());
        var serviceConstructorParameters = serviceConstructors.SelectMany(c => c.GetParameters());
        var serviceConstructorParameterTypes = serviceConstructorParameters.Select(p => p.ParameterType);

        return serviceConstructorParameterTypes.Distinct();
    }

    private static bool ConsiderGenericType(this Type type, Func<Type, bool> func) =>
        func(type) || (type.IsGenericType && func(type.GetGenericTypeDefinition()));
}