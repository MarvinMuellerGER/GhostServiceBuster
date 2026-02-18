using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

/// <summary>
/// Detects unused services based on registered dependency detectors.
/// </summary>
internal sealed class UnusedServiceDetector : IUnusedServiceDetector
{
    private readonly List<DependencyDetector> _dependencyDetectors = [];

    /// <summary>
    /// Registers a dependency detector.
    /// </summary>
    /// <param name="dependencyDetector">The detector to register.</param>
    public void RegisterDependencyDetector(DependencyDetector dependencyDetector) =>
        _dependencyDetectors.Add(dependencyDetector);
    
    /// <summary>
    /// Finds services that are unused based on root services and dependency detectors.
    /// </summary>
    /// <param name="allServices">All known services.</param>
    /// <param name="rootServices">Root services to start from.</param>
    /// <returns>The set of unused services.</returns>
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
