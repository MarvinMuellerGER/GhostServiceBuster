
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Core;

internal sealed class CoreServiceUsageVerifier : ICoreServiceUsageVerifier
{
    /// <summary>
    /// Identifies services that are not used in the dependency tree starting from root services.
    /// </summary>
    public ServiceInfoSet FindUnusedServices(in ServiceInfoSet allServices, in ServiceInfoSet rootServices)
    {
        var usedServices = rootServices.ToList();
        var unusedCandidates = allServices.Except(usedServices).ToList();

        DiscoverDependencyChain(usedServices, unusedCandidates);

        return unusedCandidates;
    }

    private static void DiscoverDependencyChain(List<ServiceInfo> usedServices, List<ServiceInfo> unusedCandidates)
    {
        IReadOnlyList<ServiceInfo> foundDependencies;
        do
        {
            foundDependencies = FindDirectDependencies(unusedCandidates, usedServices);
            MoveDependenciesToUsed(usedServices, unusedCandidates, foundDependencies);
        } while (foundDependencies.Count > 0);
    }

    private static IReadOnlyList<ServiceInfo> FindDirectDependencies(
        IReadOnlyList<ServiceInfo> potentialDependencies, 
        IReadOnlyList<ServiceInfo> currentServices)
        => potentialDependencies.GetServicesInjectedInto(currentServices);

    private static void MoveDependenciesToUsed(
        List<ServiceInfo> usedServices,
        List<ServiceInfo> unusedCandidates,
        IReadOnlyList<ServiceInfo> foundDependencies)
    {
        usedServices.AddRange(foundDependencies);
        unusedCandidates.RemoveAll(foundDependencies.Contains);
    }
}

file static class ServiceInfoExtensions
{
    /// <summary>
    /// Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    public static IReadOnlyList<ServiceInfo> GetServicesInjectedInto(
        this IReadOnlyList<ServiceInfo> sourceServices, IReadOnlyList<ServiceInfo> targetServices)
    {
        var requiredParameterTypes = targetServices.GetConstructorParameterTypes();
        
        return sourceServices
            .Where(service => IsInjectedIntoAnyOf(service, requiredParameterTypes))
            .ToArray();
    }
    
    private static bool IsInjectedIntoAnyOf(ServiceInfo service, IEnumerable<Type> requiredTypes)
    {
        return service.ServiceType.MatchesTypeOrGenericDefinition(serviceType =>
            requiredTypes.Any(requiredType => 
                requiredType.MatchesTypeOrGenericDefinition(type => type == serviceType)));
    }

    private static IEnumerable<Type> GetConstructorParameterTypes(this IReadOnlyList<ServiceInfo> services)
    {
        return services
            .SelectMany(service => service.ImplementationType.GetConstructors())
            .SelectMany(constructor => constructor.GetParameters())
            .Select(parameter => parameter.ParameterType)
            .Distinct();
    }

    private static bool MatchesTypeOrGenericDefinition(this Type type, Func<Type, bool> matcher) =>
        matcher(type) || (type.IsGenericType && matcher(type.GetGenericTypeDefinition()));
}