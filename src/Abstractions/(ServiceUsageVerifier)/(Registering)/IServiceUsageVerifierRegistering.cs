namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegistering :
    IServiceUsageVerifierRegisterServiceInfoExtractor,
    IServiceUsageVerifierRegisterDependencyDetector,
    IServiceUsageVerifierRegisterFilters,
    IServiceUsageVerifierRegisterServices;