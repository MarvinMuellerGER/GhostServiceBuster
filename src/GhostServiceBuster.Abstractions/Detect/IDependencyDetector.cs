using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

public interface IDependencyDetector
{
    /// <summary>
    ///     Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    ServiceInfoSet FindDirectDependencies(ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);
}