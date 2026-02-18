using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;

namespace GhostServiceBuster.Cache;

/// <summary>
/// Caches services and manages lazy registration actions.
/// </summary>
internal sealed class ServiceCacheHandler(IServiceInfoExtractorHandler serviceInfoExtractorHandler)
    : IServiceCacheHandler
{
    private readonly List<Action> _lazyRegisterActions = [];
    private ServiceInfoSet _services = [];

    /// <summary>
    /// Gets whether any lazy registration actions are pending.
    /// </summary>
    public bool HasAnyLazyRegisterActions => _lazyRegisterActions.Count is not 0;

    /// <summary>
    /// Clears cached services and registers the provided services.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="services">The services to register.</param>
    public void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull
    {
        _services = [];
        RegisterServices(services);
    }

    /// <summary>
    /// Registers services into the cache.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="services">The services to register.</param>
    public void RegisterServices<TServiceCollection>(in TServiceCollection? services)
        where TServiceCollection : notnull
    {
        if (services is null)
            return;

        _services = ExtractServiceInfoAndCombineWithCachedServices(services);
        NewServicesRegistered?.Invoke();
    }

    /// <summary>
    /// Registers a lazy action that provides services.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="getServicesAction">The lazy provider.</param>
    public void LazyRegisterServices<TServiceCollection>(Func<TServiceCollection>? getServicesAction)
        where TServiceCollection : notnull
    {
        if (getServicesAction is null)
            return;

        _lazyRegisterActions.Add(() => RegisterServices(getServicesAction()));
    }

    /// <summary>
    /// Gets services, combining cached and one-time services.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="oneTimeServices">Optional one-time services.</param>
    /// <returns>The combined service set.</returns>
    public ServiceInfoSet GetServices<TServiceCollection>(in TServiceCollection? oneTimeServices = default)
        where TServiceCollection : notnull
    {
        ExecuteLazyRegisterActions();

        return ExtractServiceInfoAndCombineWithCachedServices(oneTimeServices);
    }

    /// <summary>
    /// Raised when new services are registered.
    /// </summary>
    public event EventHandlerWithoutParameters? NewServicesRegistered;

    private ServiceInfoSet ExtractServiceInfoAndCombineWithCachedServices<TServiceCollection>(
        TServiceCollection? oneTimeServices) where TServiceCollection : notnull =>
        _services.Concat(serviceInfoExtractorHandler.GetServiceInfo(oneTimeServices));

    private void ExecuteLazyRegisterActions() => _lazyRegisterActions.ForEach(action => action());
}
