namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable : IServiceUsageVerifierWithoutCaches
{
    IServiceUsageVerifierWithoutCaches AsImmutable() => this;
}