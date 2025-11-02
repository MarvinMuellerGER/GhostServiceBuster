namespace GhostServiceBuster;

public interface IServiceUsageVerifier : IServiceUsageVerifierImmutable, IServiceUsageVerifierRegistering
{
    IServiceUsageVerifierImmutable AsImmutable() => this;
}