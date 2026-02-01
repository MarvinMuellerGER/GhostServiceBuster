using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
{
    new IServiceUsageVerifierWithCachedServicesMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterDependencyDetector<TDependencyDetector>();
}