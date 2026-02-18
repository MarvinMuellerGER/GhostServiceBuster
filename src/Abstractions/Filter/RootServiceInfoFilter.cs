namespace GhostServiceBuster.Filter;

/// <summary>
/// Represents root filter metadata built from a single-service filter.
/// </summary>
/// <param name="Filter">The filter to apply.</param>
/// <param name="IsIndividual">Whether the filter targets individual services.</param>
/// <param name="UseAllServices">Whether the filter should consider all services.</param>
public sealed record SingleRootServiceInfoFilterInfo(
    SingleServiceInfoFilter Filter,
    bool IsIndividual = false,
    bool UseAllServices = false) : SingleServiceInfoFilterInfo(Filter, IsIndividual);

/// <summary>
/// Represents root filter metadata for a service set filter.
/// </summary>
/// <param name="Filter">The filter to apply.</param>
/// <param name="IsIndividual">Whether the filter targets individual services.</param>
/// <param name="UseAllServices">Whether the filter should consider all services.</param>
public sealed record RootServiceInfoFilterInfo(
    ServiceInfoFilter Filter,
    bool IsIndividual = false,
    bool UseAllServices = false) : ServiceInfoFilterInfo(Filter, IsIndividual)

{
    /// <summary>
    /// Converts a tuple to a <see cref="RootServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="tuple">The tuple containing filter metadata.</param>
    public static implicit operator RootServiceInfoFilterInfo(
        (ServiceInfoFilter Filter, bool IsIndividual, bool UseAllServices) tuple) =>
        new(tuple.Filter, tuple.IsIndividual, tuple.UseAllServices);

    /// <summary>
    /// Converts a tuple to a <see cref="RootServiceInfoFilterInfo"/> using a single-service filter.
    /// </summary>
    /// <param name="tuple">The tuple containing filter metadata.</param>
    public static implicit operator RootServiceInfoFilterInfo(
        (SingleServiceInfoFilter Filter, bool IsIndividual, bool UseAllServices) tuple) =>
        new(tuple.Filter.ToServiceInfoFilter(), tuple.IsIndividual, tuple.UseAllServices);

    /// <summary>
    /// Converts a <see cref="SingleRootServiceInfoFilterInfo"/> to a <see cref="RootServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="filterInfo">The single-service filter info.</param>
    public static implicit operator RootServiceInfoFilterInfo(SingleRootServiceInfoFilterInfo filterInfo) =>
        new(filterInfo.Filter.ToServiceInfoFilter(), filterInfo.IsIndividual, filterInfo.UseAllServices);
}
