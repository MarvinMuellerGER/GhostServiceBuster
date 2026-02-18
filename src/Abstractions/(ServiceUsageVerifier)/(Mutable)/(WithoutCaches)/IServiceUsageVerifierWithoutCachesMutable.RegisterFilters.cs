using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    /// <summary>
    /// Registers filters for all, root, and unused services.
    /// </summary>
    /// <param name="allServicesFilters">Filters for all services.</param>
    /// <param name="rootServicesFilters">Filters for root services.</param>
    /// <param name="unusedServicesFilters">Filters for unused services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedFiltersMutable RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    /// <summary>
    /// Registers filter instances for all, root, and unused services.
    /// </summary>
    /// <param name="allServicesFilters">Filters for all services.</param>
    /// <param name="rootServicesFilters">Filters for root services.</param>
    /// <param name="unusedServicesFilters">Filters for unused services.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithCachedFiltersMutable RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters = null);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilterInfoList allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilter allServicesFilter, bool isIndividual = false) =>
        RegisterFilters(new ServiceInfoFilterInfo(allServicesFilter, isIndividual));

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilter(IServiceInfoFilter allServicesFilter) =>
        RegisterAllServicesFilters(allServicesFilter);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilterInfoList rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilter rootServicesFilter, bool isIndividual = false, bool useAllServices = false) =>
        RegisterFilters(
            rootServicesFilters: new RootServiceInfoFilterInfo(rootServicesFilter, isIndividual, useAllServices));

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilter(IServiceInfoFilter rootServicesFilter) =>
        RegisterRootServicesFilters(rootServicesFilter);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilterInfoList unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilter unusedServicesFilter, bool isIndividual = false) =>
        RegisterFilters(unusedServicesFilters: new ServiceInfoFilterInfo(unusedServicesFilter, isIndividual));

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilter(
        IServiceInfoFilter unusedServicesFilter) =>
        RegisterUnusedServicesFilters(unusedServicesFilter);

    /// <inheritdoc />
    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();
}
