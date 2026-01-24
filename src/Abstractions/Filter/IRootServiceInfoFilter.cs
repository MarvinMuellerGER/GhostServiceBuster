namespace GhostServiceBuster.Filter;

public interface IRootServiceInfoFilter : IServiceInfoFilter
{
    bool UseAllServices => false;
}