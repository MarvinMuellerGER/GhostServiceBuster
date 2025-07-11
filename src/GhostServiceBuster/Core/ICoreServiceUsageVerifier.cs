using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Core;

internal interface ICoreServiceUsageVerifier
{
    /// <summary>
    ///     Identifies services that are not used in the dependency chain of the root services.
    /// </summary>
    ServiceInfoSet FindUnusedServices(in ServiceInfoSet allServices, in ServiceInfoSet rootServices);
}