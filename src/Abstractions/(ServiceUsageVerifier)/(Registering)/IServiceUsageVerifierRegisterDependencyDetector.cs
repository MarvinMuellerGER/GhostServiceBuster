using GhostServiceBuster.Detect;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterDependencyDetector
{
    IServiceUsageVerifier RegisterDependencyDetector(DependencyDetector dependencyDetector);

    IServiceUsageVerifier RegisterDependencyDetector(IDependencyDetector dependencyDetector);

    IServiceUsageVerifier RegisterDependencyDetector(DependencyDetectorTupleResult dependencyDetector);
}