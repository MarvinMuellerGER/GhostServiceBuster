using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;

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

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier
        RegisterServiceInfoExtractor<TServiceCollection>(Func<TServiceCollection, ServiceInfoTuple> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollectionItem>(
        ServiceInfoExtractor<IEnumerable<TServiceCollectionItem>> extractor)
        where TServiceCollectionItem : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
    {
        if (allServicesFilters is not null)
            allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);

        if (rootServicesFilters is not null)
            rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);

        if (unusedServicesFilters is not null)
            unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices = default,
        TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        if (allServices is not null)
            allServicesAndFilterCacheHandler.RegisterServices(allServices);

        if (rootServices is not null)
            rootServicesAndFilterCacheHandler.RegisterServices(rootServices);

        return this;
    }
}