using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.NServiceBus.Filter;

file class MessageHandlerFilter : IRootServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo) =>
        serviceInfo.Where(s =>
            s.ServiceType.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>)));

    public bool IsIndividual => true;

    public bool UseAllServices => true;
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterNServiceBusMessageHandlerRootServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<MessageHandlerFilter>();
}