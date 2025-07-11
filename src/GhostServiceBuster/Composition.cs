using System.Diagnostics;
using GhostServiceBuster.Cache;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;
using GhostServiceBuster.ServiceInfoExtractor;
using Pure.DI;
using static Pure.DI.Lifetime;

namespace GhostServiceBuster;

internal sealed partial class Composition
{
    internal static Composition Instance { get; } = new();

    [Conditional("DI")]
    private void Setup() => DI.Setup()
        .RootBind<IServiceUsageVerifier>(nameof(ServiceUsageVerifier)).To<ServiceUsageVerifier>()
        .Bind<ICoreServiceUsageVerifier>().As(Singleton).To<CoreServiceUsageVerifier>()
        .Bind<IServiceInfoExtractorHandler>().As(PerResolve).To<ServiceInfoExtractorHandler>()
        .Bind<IFilterHandler>().As(Singleton).To<FilterHandler>()
        .Bind<IFilterCacheHandler>().To<FilterCacheHandler>();
}