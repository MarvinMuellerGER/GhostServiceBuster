using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.MS.Filter;

file sealed class MicrosoftAndSystemNamespacesFilter : IServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s =>
            s.ServiceType.Namespace?.StartsWith(nameof(Microsoft)) is false &&
            s.ServiceType.Namespace?.StartsWith(nameof(System)) is false);
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterMicrosoftAndSystemNamespacesAllServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterAllServicesFilter<MicrosoftAndSystemNamespacesFilter>();
}