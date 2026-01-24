namespace GhostServiceBuster.Filter;

internal static class SingleServiceInfoFilterExtensions
{
    public static ServiceInfoFilter ToServiceInfoFilter(this SingleServiceInfoFilter filter) =>
        serviceInfo => serviceInfo.Where(filter.Invoke);
}