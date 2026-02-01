using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.MS.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Extract;

file sealed class ServiceCollectionServiceInfoExtractor : IServiceInfoExtractor<IServiceCollection>
{
    public ServiceInfoSet ExtractServiceInfos(IServiceCollection serviceProvider) => serviceProvider.ToServiceInfos();
}

file sealed class ServiceProviderServiceInfoExtractor : IServiceInfoExtractor<IServiceProvider>
{
    public ServiceInfoSet ExtractServiceInfos(IServiceProvider serviceProvider) =>
        serviceProvider.GetAllServiceDescriptorsUnsafe().ToServiceInfos();
}

file static class ServiceDescriptorEnumerableExtensions
{
    public static ServiceInfoSet ToServiceInfos(this IEnumerable<ServiceDescriptor> serviceDescriptors) =>
        serviceDescriptors.Select(serviceDescriptor =>
            new ServiceInfo(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType)).ToServiceInfoSet();
}

public static class ServiceUsageVerifierExtensions
{
    public static TServiceUsageVerifier RegisterServiceCollectionServiceInfoExtractor<TServiceUsageVerifier>(
        this TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable =>
        (TServiceUsageVerifier)serviceUsageVerifier
            .RegisterServiceInfoExtractor<ServiceCollectionServiceInfoExtractor, IServiceCollection>()
            .RegisterServiceInfoExtractor<ServiceProviderServiceInfoExtractor, IServiceProvider>();
}