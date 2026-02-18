using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesAndFiltersMutable
{
    /// <summary>
    /// Registers a dependency detector of the specified type.
    /// </summary>
    /// <typeparam name="TDependencyDetector">The dependency detector type.</typeparam>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterDependencyDetector<TDependencyDetector>();
}
