using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

internal sealed partial class ServiceUsageVerifier
{
    public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable
        RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(DependencyDetector dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector<TDependencyDetector>()
        where TDependencyDetector : IDependencyDetector, new()
    {
        unusedServiceDetector.RegisterDependencyDetector<TDependencyDetector>();

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(IDependencyDetector dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifierWithoutCachesMutable RegisterDependencyDetector(
        DependencyDetectorTupleResult dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifierWithCachedFiltersMutable RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters,
        ServiceInfoFilterInfoList? rootServicesFilters,
        ServiceInfoFilterInfoList? unusedServicesFilters)
    {
        allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);
        rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);
        unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    IServiceUsageVerifierWithCachedFiltersMutable IServiceUsageVerifierWithoutCachesMutable.RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters)
    {
        allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);
        rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);
        unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        allServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        rootServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        unusedServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifierWithCachedServicesMutable
        RegisterServices<TAllServicesCollection, TRootServicesCollection>(
            TAllServicesCollection? allServices,
            TRootServicesCollection? rootServices)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        allServicesCacheHandler.RegisterServices(allServices);
        rootServicesCacheHandler.RegisterServices(rootServices);

        return this;
    }

    public IServiceUsageVerifierWithCachedServicesMutable
        LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
            Func<TAllServicesCollection>? getAllServicesAction = null,
            Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        allServicesCacheHandler.LazyRegisterServices(getAllServicesAction);
        rootServicesCacheHandler.LazyRegisterServices(getRootServicesAction);

        return this;
    }
}