namespace GhostServiceBuster;

public static class Verify
{
    public static IServiceUsageVerifierWithoutCachesMutable New => Composition.Instance.ServiceUsageVerifier;
}