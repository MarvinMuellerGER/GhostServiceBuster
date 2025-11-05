using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

public interface IServiceInfoFilter
{
    bool IsIndividual => false;

    ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfo);
}