using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.RegisterMethodsGenerator;

namespace GhostServiceBuster.NServiceBus.Filter;

[GenerateRegisterMethodFor]
internal class NServiceBusMessageHandlerFilter : IRootServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo) =>
        serviceInfo.Where(s =>
            s.ServiceType.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>)));

    public bool IsIndividual => true;

    public bool UseAllServices => true;
}