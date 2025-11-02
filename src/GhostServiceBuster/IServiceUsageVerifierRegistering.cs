namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegistering :
    IServiceUsageVerifierRegisterServiceInfoExtractor,
    IServiceUsageVerifierRegisterFilters,
    IServiceUsageVerifierRegisterServices;