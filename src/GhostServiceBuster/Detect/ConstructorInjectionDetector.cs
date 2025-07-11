namespace GhostServiceBuster.Detect;

internal sealed class ConstructorInjectionDetector : IDependencyDetector
{
    /// <summary>
    ///     Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    public IReadOnlyList<ServiceInfo> FindDirectDependencies(
        IReadOnlyList<ServiceInfo> servicesToAnalyse, IReadOnlyList<ServiceInfo> potentialDependencies)
    {
        var injectedTypes = GetConstructorParameterTypes(servicesToAnalyse);

        return potentialDependencies
            .Where(service => IsOfAnyOfTheseTypes(service, injectedTypes))
            .ToArray();
    }

    private static IEnumerable<Type> GetConstructorParameterTypes(IReadOnlyList<ServiceInfo> services) =>
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