using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

internal sealed partial class ServiceUsageVerifier
{
    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterDependencyDetector(DependencyDetector dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifier RegisterDependencyDetector(IDependencyDetector dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifier RegisterDependencyDetector(DependencyDetectorTupleResult dependencyDetector)
    {
        unusedServiceDetector.RegisterDependencyDetector(dependencyDetector);

        return this;
    }

    public IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters,
        ServiceInfoFilterInfoList? rootServicesFilters,
        ServiceInfoFilterInfoList? unusedServicesFilters)
    {
        if (allServicesFilters is not null)
            allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);

        if (rootServicesFilters is not null)
            rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);

        if (unusedServicesFilters is not null)
            unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    IServiceUsageVerifier IServiceUsageVerifierRegisterFilters.RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters)
    {
        if (allServicesFilters is not null)
            allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);

        if (rootServicesFilters is not null)
            rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);

        if (unusedServicesFilters is not null)
            unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        allServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifier RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        allServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifier RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new()
    {
        allServicesFilterCacheHandler.RegisterFilter<TServiceInfoFilter>();

        return this;
    }

    public IServiceUsageVerifier RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices,
        TRootServicesCollection? rootServices)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        if (allServices is not null)
            allServicesAndFilterCacheHandler.RegisterServices(allServices);

        if (rootServices is not null)
            rootServicesAndFilterCacheHandler.RegisterServices(rootServices);

        return this;
    }

    public IServiceUsageVerifier LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
        Func<TAllServicesCollection>? getAllServicesAction = null,
        Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        if (getAllServicesAction is not null)
            allServicesAndFilterCacheHandler.LazyRegisterServices(getAllServicesAction);

        if (getRootServicesAction is not null)
            rootServicesAndFilterCacheHandler.LazyRegisterServices(getRootServicesAction);

        return this;
    }

    public IServiceUsageVerifier UseAllServicesAsRootServices()
    {
        _useAllServicesAsRootServices = true;
        rootServicesAndFilterCacheHandler.ReplaceServiceCacheHandler(allServicesCacheHandler);

        return this;
    }
}