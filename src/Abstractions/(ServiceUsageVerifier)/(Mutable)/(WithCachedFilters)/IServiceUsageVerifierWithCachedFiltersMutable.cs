namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithCachedFiltersMutable
    : IServiceUsageVerifierWithCachedFilters, IServiceUsageVerifierWithoutCachesMutable;

/// <summary>
/// Provides extension methods for service usage verifiers with cached filters.
/// </summary>
public static partial class ServiceUsageVerifierExtensions
{
    /// <summary>
    /// Returns an immutable view of the verifier.
    /// </summary>
    /// <param name="serviceUsageVerifier">The verifier to convert.</param>
    /// <returns>The immutable verifier view.</returns>
    public static IServiceUsageVerifierWithCachedFilters AsImmutable(
        this IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier) =>
        serviceUsageVerifier;
}
