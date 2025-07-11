using System.Diagnostics;
using GhostServiceBuster.Cache;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Lifetime;

namespace GhostServiceBuster;

internal sealed partial class Composition
{
    internal static Composition Instance { get; } = new();

    [Conditional("DI")]
    // ReSharper disable once UnusedMember.Local
    private static void Setup() => DI.Setup().Hint(Hint.Resolve, "OFF")
        .RootBind<IServiceUsageVerifier>(nameof(ServiceUsageVerifier)).To<ServiceUsageVerifier>()
        .Bind<IUnusedServiceDetector>().As(Singleton).To<UnusedServiceDetector>()
        .Bind<IDependencyDetector>().As(Singleton).To<ConstructorInjectionDetector>()
        .Bind<IServiceInfoExtractorHandler>().As(PerResolve).To<ServiceInfoExtractorHandler>()
        .Bind<IFilterHandler>().As(Singleton).To<FilterHandler>()
        .Bind<IFilterCacheHandler>().To<FilterCacheHandler>();
}