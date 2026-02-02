#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Filter
{
    public interface IServiceInfoFilter
    {
#if NET
    bool IsIndividual => false;

    ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo);
#endif
    }
}