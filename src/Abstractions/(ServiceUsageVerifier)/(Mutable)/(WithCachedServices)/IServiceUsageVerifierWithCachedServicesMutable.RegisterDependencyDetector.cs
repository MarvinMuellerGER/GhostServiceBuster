using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
{
    /// <summary>
    /// Registers a dependency detector of the specified type.
    /// </summary>
    /// <typeparam name="TDependencyDetector">The dependency detector type.</typeparam>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterDependencyDetector<TDependencyDetector>();
}
