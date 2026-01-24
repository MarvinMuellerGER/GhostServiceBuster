namespace GhostServiceBuster.Filter;

public sealed record SingleRootServiceInfoFilterInfo(
    SingleServiceInfoFilter Filter,
    bool IsIndividual = false,
    bool UseAllServices = false) : SingleServiceInfoFilterInfo(Filter, IsIndividual);

public sealed record RootServiceInfoFilterInfo(
    ServiceInfoFilter Filter,
    bool IsIndividual = false,
    bool UseAllServices = false) : ServiceInfoFilterInfo(Filter, IsIndividual)

{
    public static implicit operator RootServiceInfoFilterInfo(
        (ServiceInfoFilter Filter, bool IsIndividual, bool UseAllServices) tuple) =>
        new(tuple.Filter, tuple.IsIndividual, tuple.UseAllServices);

    public static implicit operator RootServiceInfoFilterInfo(
        (SingleServiceInfoFilter Filter, bool IsIndividual, bool UseAllServices) tuple) =>
        new(tuple.Filter.ToServiceInfoFilter(), tuple.IsIndividual, tuple.UseAllServices);

    public static implicit operator RootServiceInfoFilterInfo(SingleRootServiceInfoFilterInfo filterInfo) =>
        new(filterInfo.Filter.ToServiceInfoFilter(), filterInfo.IsIndividual, filterInfo.UseAllServices);
}