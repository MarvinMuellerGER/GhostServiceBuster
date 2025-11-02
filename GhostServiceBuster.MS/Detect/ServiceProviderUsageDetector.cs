using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.MS.Detect;

file sealed class ServiceProviderUsageDetector : IDependencyDetector
{
    public async Task<ServiceInfoSet> FindDirectDependencies(
        ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies)
    {
        return await Task.FromResult<ServiceInfoSet>([]);
    }
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterServiceProviderUsageDetector(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterDependencyDetector(new ServiceProviderUsageDetector());
}