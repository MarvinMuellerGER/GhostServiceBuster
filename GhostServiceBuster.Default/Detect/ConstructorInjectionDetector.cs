using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Default.Detect;

file sealed class ConstructorInjectionDetector : IDependencyDetector
{
    /// <summary>
    ///     Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    public Task<ServiceInfoSet> FindDirectDependencies(ServiceInfoSet servicesToAnalyse,
        ServiceInfoSet potentialDependencies)
    {
        var injectedTypes = GetConstructorParameterTypes(servicesToAnalyse);

        return Task.FromResult(potentialDependencies.Where(service => IsOfAnyOfTheseTypes(service, injectedTypes)));
    }

    private static IEnumerable<Type> GetConstructorParameterTypes(ServiceInfoSet services) =>
        services
            .SelectMany(service => service.ImplementationType.GetConstructors())
            .SelectMany(constructor => constructor.GetParameters())
            .Select(parameter => parameter.ParameterType)
            .Distinct();

    private static bool IsOfAnyOfTheseTypes(ServiceInfo service, IEnumerable<Type> requiredTypes) =>
        MatchesTypeOrGenericDefinition(service.ServiceType, serviceType =>
            requiredTypes.Any(requiredType =>
                MatchesTypeOrGenericDefinition(requiredType, type => type == serviceType)));

    private static bool MatchesTypeOrGenericDefinition(Type type, Func<Type, bool> matcher) =>
        matcher(type) || (type.IsGenericType && matcher(type.GetGenericTypeDefinition()));
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterConstructorInjectionDetector(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterDependencyDetector(new ConstructorInjectionDetector());
}