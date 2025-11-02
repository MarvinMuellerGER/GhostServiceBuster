using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

internal sealed class UnusedServiceDetector(IDependencyDetector constructorInjectionDetector)
    : IUnusedServiceDetector
{
    /// <summary>
    ///     Identifies services that are not used in the dependency tree starting from root services.
    /// </summary>
    public ServiceInfoSet FindUnusedServices(in ServiceInfoSet allServices, in ServiceInfoSet rootServices)
    {
        var usedServices = rootServices.ToList();
        var unusedCandidates = allServices.Except(usedServices).ToList();

        DiscoverDependencyChain(usedServices, unusedCandidates);

        return unusedCandidates;
    }

    private void DiscoverDependencyChain(List<ServiceInfo> usedServices, List<ServiceInfo> unusedCandidates)
    {
        IReadOnlyList<ServiceInfo> foundDependencies;
        do
        {
            foundDependencies = constructorInjectionDetector.FindDirectDependencies(usedServices, unusedCandidates);
            MoveDependenciesToUsed(usedServices, unusedCandidates, foundDependencies);
        } while (foundDependencies.Count > 0);
    }

    private static void MoveDependenciesToUsed(
        List<ServiceInfo> usedServices,
        List<ServiceInfo> unusedCandidates,
        IReadOnlyList<ServiceInfo> foundDependencies)
    {
        usedServices.AddRange(foundDependencies);
        unusedCandidates.RemoveAll(foundDependencies.Contains);
    }
}