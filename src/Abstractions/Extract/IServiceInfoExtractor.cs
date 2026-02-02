#if NET
using GhostServiceBuster.Collections;
#endif

namespace GhostServiceBuster.Extract
{
    public interface IServiceInfoExtractor<in TServiceCollection>
    {
#if NET
    ServiceInfoSet ExtractServiceInfos(TServiceCollection serviceProvider);
#endif
    }
}