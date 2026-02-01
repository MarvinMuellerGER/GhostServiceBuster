namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedFiltersMutable
    : IServiceUsageVerifierWithCachedFilters, IServiceUsageVerifierWithoutCachesMutable;

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedFilters AsImmutable(
        this IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier) =>
        serviceUsageVerifier;
}