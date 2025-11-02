using System.Diagnostics;
using GhostServiceBuster.Cache;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Lifetime;
using static Pure.DI.Tag;

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
        .Bind<IServiceCacheHandler>(All).As(PerResolve).To<ServiceCacheHandler>()
        .Bind<IServiceCacheHandler>(Root).As(PerResolve).To<ServiceCacheHandler>()
        .Bind<IServiceCacheHandler>(Unused).As(PerResolve).To<ServiceCacheHandler>()
        .Bind<IFilterCacheHandler>(All).As(PerResolve).To<FilterCacheHandler>()
        .Bind<IFilterCacheHandler>(Root).As(PerResolve).To<FilterCacheHandler>()
        .Bind<IFilterCacheHandler>(Unused).As(PerResolve).To<FilterCacheHandler>()
        .Bind<IServiceAndFilterCacheHandler>(All).As(PerResolve).To<ServiceAndFilterCacheHandler>(ctx =>
        {
            ctx.Inject<IServiceCacheHandler>(All, out var serviceCacheHandler);
            ctx.Inject<IFilterCacheHandler>(All, out var filterCacheHandler);

            return new ServiceAndFilterCacheHandler(serviceCacheHandler, filterCacheHandler);
        })
        .Bind<IServiceAndFilterCacheHandler>(Root).As(PerResolve).To<ServiceAndFilterCacheHandler>(ctx =>
        {
            ctx.Inject<IServiceCacheHandler>(Root, out var serviceCacheHandler);
            ctx.Inject<IFilterCacheHandler>(Root, out var filterCacheHandler);

            return new ServiceAndFilterCacheHandler(serviceCacheHandler, filterCacheHandler);
        })
        .Bind<IServiceAndFilterCacheHandler>(Unused).As(PerResolve).To<ServiceAndFilterCacheHandler>(ctx =>
        {
            ctx.Inject<IServiceCacheHandler>(Unused, out var serviceCacheHandler);
            ctx.Inject<IFilterCacheHandler>(Unused, out var filterCacheHandler);

            return new ServiceAndFilterCacheHandler(serviceCacheHandler, filterCacheHandler);
        });
}