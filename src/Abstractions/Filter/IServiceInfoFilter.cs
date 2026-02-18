#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Filter
{
    /// <summary>
    /// Defines a filter that can transform a set of services.
    /// </summary>
    public interface IServiceInfoFilter
    {
#if NET
        /// <summary>
        /// Gets whether the filter is applied to individual services.
        /// </summary>
        bool IsIndividual => false;

        /// <summary>
        /// Filters the supplied services.
        /// </summary>
        /// <param name="serviceInfo">The services to filter.</param>
        /// <returns>The filtered services.</returns>
        ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo);
#endif
    }
}
