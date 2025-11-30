using System.Diagnostics.CodeAnalysis;
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance.")]
internal sealed class UnusedServiceDetector : IUnusedServiceDetector
{
    private readonly List<DependencyDetector> _dependencyDetectors = [];

    public void RegisterDependencyDetector(DependencyDetector dependencyDetector) =>
        _dependencyDetectors.Add(dependencyDetector);

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
        ServiceInfoSet foundDependencies;
        do
        {
            foundDependencies = FindDirectDependencies(usedServices, unusedCandidates);
            MoveDependenciesToUsed(usedServices, unusedCandidates, foundDependencies);
        } while (foundDependencies.Count > 0);
    }

    private static void MoveDependenciesToUsed(
        List<ServiceInfo> usedServices,
        List<ServiceInfo> unusedCandidates,
        ServiceInfoSet foundDependencies)
    {
        usedServices.AddRange(foundDependencies);
        unusedCandidates.RemoveAll(foundDependencies.Contains);
    }

    private ServiceInfoSet FindDirectDependencies(
        ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies) =>
        potentialDependencies.Except(
            _dependencyDetectors.Aggregate(potentialDependencies,
                (remainingPotentialDependencies, dependencyDetector) => remainingPotentialDependencies
                    .Except(dependencyDetector(servicesToAnalyse, remainingPotentialDependencies))
                    .ToList()));
}