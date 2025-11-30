using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Extract;

file sealed class ServiceCollectionServiceInfoExtractor : IServiceInfoExtractor<IServiceCollection>
{
    public ServiceInfoSet ExtractServiceInfos(IServiceCollection serviceProvider) =>
        serviceProvider.Select(serviceDescriptor =>
            new ServiceInfo(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType)).ToServiceInfoSet();
}

file sealed class ServiceProviderServiceInfoExtractor : IServiceInfoExtractor<IServiceProvider>
{
    private static readonly ServiceCollectionServiceInfoExtractor ServiceCollectionServiceInfoExtractor = new();

    public ServiceInfoSet ExtractServiceInfos(IServiceProvider serviceProvider) =>
        ServiceCollectionServiceInfoExtractor.ExtractServiceInfos(
            serviceProvider.GetRequiredService<IServiceCollection>());
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterServiceCollectionServiceInfoExtractor(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier
            .RegisterServiceInfoExtractor<ServiceCollectionServiceInfoExtractor, IServiceCollection>()
            .RegisterServiceInfoExtractor<ServiceProviderServiceInfoExtractor, IServiceProvider>();
}