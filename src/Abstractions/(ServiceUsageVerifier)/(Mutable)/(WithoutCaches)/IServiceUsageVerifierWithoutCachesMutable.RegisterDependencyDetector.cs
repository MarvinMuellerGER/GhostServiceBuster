using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    /// <summary>
    /// Registers a dependency detector delegate.
    /// </summary>
    /// <param name="dependencyDetector">The detector delegate.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        DependencyDetector dependencyDetector);

    /// <summary>
    /// Registers a dependency detector by type.
    /// </summary>
    /// <typeparam name="TDependencyDetector">The dependency detector type.</typeparam>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new();

    /// <summary>
    /// Registers a dependency detector instance.
    /// </summary>
    /// <param name="dependencyDetector">The detector instance.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        IDependencyDetector dependencyDetector);

    /// <summary>
    /// Registers a dependency detector tuple delegate.
    /// </summary>
    /// <param name="dependencyDetector">The detector delegate.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        DependencyDetectorTupleResult dependencyDetector);
}

public static partial class ServiceUsageVerifierExtensions
{
    /// <summary>
    /// Registers a dependency detector by type.
    /// </summary>
    /// <typeparam name="TDependencyDetector">The dependency detector type.</typeparam>
    /// <param name="serviceUsageVerifier">The verifier to register with.</param>
    /// <returns>The updated verifier.</returns>
    public static IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector<TDependencyDetector>(
        this IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
            .RegisterDependencyDetector<TDependencyDetector>();
    
    extension<TServiceUsageVerifier>(TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable
    {
        /// <summary>
        /// Registers a dependency detector delegate.
        /// </summary>
        /// <param name="dependencyDetector">The detector delegate.</param>
        /// <returns>The updated verifier.</returns>
        public TServiceUsageVerifier RegisterDependencyDetector(DependencyDetector dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);

        /// <summary>
        /// Registers a dependency detector instance.
        /// </summary>
        /// <param name="dependencyDetector">The detector instance.</param>
        /// <returns>The updated verifier.</returns>
        public TServiceUsageVerifier RegisterDependencyDetector(IDependencyDetector dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);

        /// <summary>
        /// Registers a dependency detector tuple delegate.
        /// </summary>
        /// <param name="dependencyDetector">The detector delegate.</param>
        /// <returns>The updated verifier.</returns>
        public TServiceUsageVerifier RegisterDependencyDetector(DependencyDetectorTupleResult dependencyDetector) =>
            (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector(dependencyDetector);
    }
}
