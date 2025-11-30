namespace GhostServiceBuster;

public static class Verify
{
    public static IServiceUsageVerifier New => Composition.Instance.ServiceUsageVerifier;
}