namespace GhostServiceBuster;

public interface IServiceUsageVerifier : IServiceUsageVerifierImmutable, IServiceUsageVerifierRegistering
{
    static IServiceUsageVerifier New => Composition.Instance.ServiceUsageVerifier;

    IServiceUsageVerifierImmutable AsImmutable() => this;
}