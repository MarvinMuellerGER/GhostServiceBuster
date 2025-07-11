using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifier : IImmutableServiceUsageVerifier
{
    static IServiceUsageVerifier New => Composition.Instance.ServiceUsageVerifier;

    IImmutableServiceUsageVerifier AsImmutable() => this;

    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilterInfoList allServicesFilters)
        => RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilterInfoList rootServicesFilters)
        => RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilterInfoList unusedServicesFilters)
        => RegisterFilters(unusedServicesFilters: unusedServicesFilters);
}

public interface IImmutableServiceUsageVerifier
    : IServiceUsageVerifierWithoutCachedFilters, IServiceUsageVerifierWithCachedFilters;

public interface IServiceUsageVerifierWithoutCachedFilters : IServiceUsageVerifierWithoutCachedFiltersFluently
{
    ServiceInfoSet GetUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        GetUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, out var unusedServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet GetUnusedServicesUsingOnlyOneTimeAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, allServicesFilters);

    ServiceInfoSet GetUnusedServicesUsingOnlyOneTimeRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, rootServicesFilters: rootServicesFilters);

    ServiceInfoSet GetUnusedServicesUsingOnlyOneTimeUnusedServicesFilters
        <TAllServicesCollection, TRootServicesCollection>(
            in TAllServicesCollection allServices,
            in TRootServicesCollection rootServices,
            params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, unusedServicesFilters: unusedServicesFilters);
}

public interface IServiceUsageVerifierWithoutCachedFiltersFluently
{
    IServiceUsageVerifier GetUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier GetUnusedServicesUsingOnlyOneTimeAllServicesFilters<TAllServicesCollection,
        TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, out unusedServices, allServicesFilters);

    IServiceUsageVerifier GetUnusedServicesUsingOnlyOneTimeRootServicesFilters<TAllServicesCollection,
        TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(
            in allServices, in rootServices, out unusedServices, rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier GetUnusedServicesUsingOnlyOneTimeUnusedServicesFilters
        <TAllServicesCollection, TRootServicesCollection>(
            in TAllServicesCollection allServices,
            in TRootServicesCollection rootServices,
            out ServiceInfoSet unusedServices,
            params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServicesUsingOnlyOneTimeFilters(
            in allServices, in rootServices, out unusedServices, unusedServicesFilters: unusedServicesFilters);
}

public interface IServiceUsageVerifierWithCachedFilters : IServiceUsageVerifierWithCachedFiltersFluently
{
    ServiceInfoSet GetUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        GetUnusedServices(in allServices, in rootServices, out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet GetUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeAllServicesFilters);

    ServiceInfoSet GetUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeRootServicesFilters);

    ServiceInfoSet GetUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeUnusedServicesFilters);
}

public interface IServiceUsageVerifierWithCachedFiltersFluently
{
    IServiceUsageVerifier GetUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier GetUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeAllServicesFilters);

    IServiceUsageVerifier GetUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeRootServicesFilters);

    IServiceUsageVerifier GetUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeUnusedServicesFilters);
}