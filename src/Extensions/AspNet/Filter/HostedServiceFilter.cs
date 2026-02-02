using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.RegisterMethodsGenerator;
using Microsoft.Extensions.Hosting;

namespace GhostServiceBuster.AspNet.Filter;

[GenerateRegisterMethodFor]
internal sealed class HostedServiceFilter : IRootServiceInfoFilter
{
    public bool IsIndividual => true;

    public bool UseAllServices => true;

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => s.ServiceType == typeof(IHostedService));
}