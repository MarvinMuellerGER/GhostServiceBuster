using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

public interface IServiceInfoExtractor<in TServiceCollection>
{
    public ServiceInfoSet ExtractServiceInfos(TServiceCollection serviceCollection);
}