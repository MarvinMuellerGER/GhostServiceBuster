#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Extract
{
    /// <summary>
    /// Defines a service info extractor for a specific service collection type.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type to extract from.</typeparam>
    public interface IServiceInfoExtractor<in TServiceCollection>
    {
#if NET
        /// <summary>
        /// Extracts service information from the provided collection.
        /// </summary>
        /// <param name="serviceProvider">The service collection to inspect.</param>
        /// <returns>The extracted service information.</returns>
        ServiceInfoSet ExtractServiceInfos(TServiceCollection serviceProvider);
#endif
    }
}
