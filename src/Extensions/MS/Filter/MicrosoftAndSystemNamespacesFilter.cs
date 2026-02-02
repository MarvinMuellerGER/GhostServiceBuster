using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.RegisterMethodsGenerator;

namespace GhostServiceBuster.MS.Filter;

[GenerateRegisterMethodForFilter(FilterKind.AllServicesFilter)]
internal sealed class MicrosoftAndSystemNamespacesFilter : IServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s =>
            s.ServiceType.Namespace?.StartsWith(nameof(Microsoft)) is false &&
            s.ServiceType.Namespace?.StartsWith(nameof(System)) is false);
}