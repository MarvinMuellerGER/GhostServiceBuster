namespace GhostServiceBuster.Filter
{
    /// <summary>
    /// Defines a filter that targets root services.
    /// </summary>
    public interface IRootServiceInfoFilter : IServiceInfoFilter
    {
#if NET
        /// <summary>
        /// Gets whether the filter should consider all services instead of root-only services.
        /// </summary>
        bool UseAllServices => false;
#endif
    }
}
