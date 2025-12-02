using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.NServiceBus.Filter;

file class MessageHandlerFilter : IServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo) =>
        serviceInfo.Where(s => s.ServiceType.IsAssignableTo(typeof(IHandleMessages<>)));
    
    public bool IsIndividual => true;
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterNServiceBusMessageHandlerRootServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<MessageHandlerFilter>();
}