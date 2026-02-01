namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
    : IServiceUsageVerifierWithCachedServices, IServiceUsageVerifierWithoutCachesMutable
{
    new IServiceUsageVerifierWithCachedServices AsImmutable() => this;
}