namespace GhostServiceBuster.Core;

public interface IServiceUsageVerifier
{
    IReadOnlySet<ServiceInfo> GetUnusedServices(
        in IReadOnlySet<ServiceInfo> allServices, in IReadOnlySet<ServiceInfo> rootServices);
}