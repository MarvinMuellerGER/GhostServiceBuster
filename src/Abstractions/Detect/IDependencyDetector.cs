#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Detect
{
    /// <summary>
    /// Defines a component that discovers service dependencies.
    /// </summary>
    public interface IDependencyDetector
    {
#if NET
        /// <summary>
        /// Finds services from the source list that are injected into any service in the target list.
        /// </summary>
        /// <param name="servicesToAnalyse">The services whose dependencies should be evaluated.</param>
        /// <param name="potentialDependencies">The services that can be considered dependencies.</param>
        /// <returns>The set of dependencies found in the target list.</returns>
        Task<ServiceInfoSet> FindDirectDependencies(
            ServiceInfoSet servicesToAnalyse,
            ServiceInfoSet potentialDependencies);
#endif
    }
}
