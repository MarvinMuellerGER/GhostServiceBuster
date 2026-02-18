using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
{
    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        => (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
            .RegisterFilters(allServicesFilters, rootServicesFilters, unusedServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilterInfoList allServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilter allServicesFilter, bool isIndividual = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilter, isIndividual);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilter(
        IServiceInfoFilter allServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilter(allServicesFilter);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilter<TServiceInfoFilter>();

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilterInfoList rootServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilter rootServicesFilter, bool isIndividual = false, bool useAllServices = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilter, isIndividual, useAllServices);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilter(
        IServiceInfoFilter rootServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilter(rootServicesFilter);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilter<TServiceInfoFilter>();

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilterInfoList unusedServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilter unusedServicesFilter, bool isIndividual = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilter, isIndividual);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilters);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilter(
        IServiceInfoFilter unusedServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilter(unusedServicesFilter);

    /// <inheritdoc />
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilter<TServiceInfoFilter>();
}
