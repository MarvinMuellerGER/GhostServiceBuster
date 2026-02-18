using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public static partial class ServiceUsageVerifierExtensions
{
    /// <summary>
    /// Registers a dependency detector of the specified type.
    /// </summary>
    /// <typeparam name="TDependencyDetector">The dependency detector type.</typeparam>
    /// <param name="serviceUsageVerifier">The verifier to register with.</param>
    /// <returns>The updated verifier.</returns>
    public static IServiceUsageVerifierWithCachedFiltersMutable RegisterDependencyDetector<TDependencyDetector>(
        this IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier)
        where TDependencyDetector : IDependencyDetector, new() =>
        (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
            .RegisterDependencyDetector<TDependencyDetector>();
}
