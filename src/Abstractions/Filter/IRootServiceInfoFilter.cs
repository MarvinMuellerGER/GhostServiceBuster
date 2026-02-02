namespace GhostServiceBuster.Filter
{
    public interface IRootServiceInfoFilter : IServiceInfoFilter
    {
#if NET
    bool UseAllServices => false;
#endif
    }
}