using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.MS.Utils;
using GhostServiceBuster.RegisterMethodsGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Extract;

[GenerateRegisterMethodFor]
internal sealed class ServiceCollectionServiceInfoExtractor : IServiceInfoExtractor<IServiceCollection>
{
    public ServiceInfoSet ExtractServiceInfos(IServiceCollection serviceProvider) => serviceProvider.ToServiceInfos();
}

[GenerateRegisterMethodFor]
internal sealed class ServiceProviderServiceInfoExtractor : IServiceInfoExtractor<IServiceProvider>
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