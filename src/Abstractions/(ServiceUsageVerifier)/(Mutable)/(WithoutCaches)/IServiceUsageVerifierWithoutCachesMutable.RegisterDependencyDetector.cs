using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        DependencyDetector dependencyDetector);

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new();

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        IDependencyDetector dependencyDetector);

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        DependencyDetectorTupleResult dependencyDetector);
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector<TDependencyDetector>(
        this IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
            .RegisterDependencyDetector<TDependencyDetector>();
    
    extension<TServiceUsageVerifier>(TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable
    {
        public TServiceUsageVerifier RegisterDependencyDetector(DependencyDetector dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);

        public TServiceUsageVerifier RegisterDependencyDetector(IDependencyDetector dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);

        public TServiceUsageVerifier RegisterDependencyDetector(DependencyDetectorTupleResult dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);
    }
}