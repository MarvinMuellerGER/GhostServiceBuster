using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Core;

internal interface ICoreServiceUsageVerifier
{
    ServiceInfoSet GetUnusedServices(
        in ServiceInfoSet allServices, in ServiceInfoSet rootServices);
}