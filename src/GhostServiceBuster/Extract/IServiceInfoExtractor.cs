using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

public interface IServiceInfoExtractor<in TServiceCollection>
{
    ServiceInfoSet ExtractServiceInfos(TServiceCollection serviceCollection);
}