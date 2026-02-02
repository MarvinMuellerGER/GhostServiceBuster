#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Detect
{
    public interface IDependencyDetector
    {
#if NET
    /// <summary>
    ///     Finds services from the source list that are injected into any service in the target list.
    /// </summary>
    Task<ServiceInfoSet> FindDirectDependencies(ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);
#endif
    }
}