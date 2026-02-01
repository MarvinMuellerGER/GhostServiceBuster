using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedFiltersMutable RegisterDependencyDetector<TDependencyDetector>(
        this IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier)
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
            .RegisterDependencyDetector<TDependencyDetector>();
}