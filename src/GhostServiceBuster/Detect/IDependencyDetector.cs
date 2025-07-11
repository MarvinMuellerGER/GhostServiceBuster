namespace GhostServiceBuster.Detect;

internal interface IDependencyDetector
{
    /// <summary>
    ///     Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    IReadOnlyList<ServiceInfo> FindDirectDependencies(IReadOnlyList<ServiceInfo> servicesToAnalyse,
        IReadOnlyList<ServiceInfo> potentialDependencies);
}