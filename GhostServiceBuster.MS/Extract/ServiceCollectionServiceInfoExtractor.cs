using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Extract;

file sealed class ServiceCollectionServiceInfoExtractor : IServiceInfoExtractor<IServiceCollection>
{
    public ServiceInfoSet ExtractServiceInfos(IServiceCollection serviceCollection) =>
        serviceCollection.Select(serviceDescriptor =>
            new ServiceInfo(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType)).ToServiceInfoSet();
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterServiceCollectionServiceInfoExtractor(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterServiceInfoExtractor<ServiceCollectionServiceInfoExtractor, IServiceCollection>();
}