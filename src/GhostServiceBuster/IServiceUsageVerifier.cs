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
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, out var unusedServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, allServicesFilters);

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, rootServicesFilters: rootServicesFilters);

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeUnusedServicesFilters
        <TAllServicesCollection, TRootServicesCollection>(
            in TAllServicesCollection allServices,
            in TRootServicesCollection rootServices,
            params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, unusedServicesFilters: unusedServicesFilters);
}

public interface IServiceUsageVerifierWithoutCachedFiltersFluently
{
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeAllServicesFilters<TAllServicesCollection,
        TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(in allServices, in rootServices, out unusedServices, allServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeRootServicesFilters<TAllServicesCollection,
        TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(
            in allServices, in rootServices, out unusedServices, rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeUnusedServicesFilters
        <TAllServicesCollection, TRootServicesCollection>(
            in TAllServicesCollection allServices,
            in TRootServicesCollection rootServices,
            out ServiceInfoSet unusedServices,
            params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeFilters(
            in allServices, in rootServices, out unusedServices, unusedServicesFilters: unusedServicesFilters);
}

public interface IServiceUsageVerifierWithCachedFilters : IServiceUsageVerifierWithCachedFiltersFluently
{
    ServiceInfoSet FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServices(in allServices, in rootServices, out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, oneTimeAllServicesFilters);

    ServiceInfoSet FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, oneTimeRootServicesFilters);

    ServiceInfoSet FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, oneTimeUnusedServicesFilters);
}

public interface IServiceUsageVerifierWithCachedFiltersFluently
{
    IServiceUsageVerifier FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeAllServicesFilters);

    IServiceUsageVerifier FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeRootServicesFilters);

    IServiceUsageVerifier FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(in allServices, in rootServices, out unusedServices, oneTimeUnusedServicesFilters);
}