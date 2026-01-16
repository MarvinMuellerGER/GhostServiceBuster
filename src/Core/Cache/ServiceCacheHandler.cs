using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;

namespace GhostServiceBuster.Cache;

internal sealed class ServiceCacheHandler(IServiceInfoExtractorHandler serviceInfoExtractorHandler)
    : IServiceCacheHandler
{
    private readonly List<Action> _lazyRegisterActions = [];
    private ServiceInfoSet _services = [];

    public bool HasAnyLazyRegisterActions => _lazyRegisterActions.Count is not 0;

    public void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull
    {
        _services = [];
        RegisterServices(services);
    }

    public void RegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull
    {
        _services = ExtractServiceInfoAndCombineWithCachedServices(services);
        NewServicesRegistered?.Invoke();
    }

    public void LazyRegisterServices<TServiceCollection>(Func<TServiceCollection> getServicesAction)
        where TServiceCollection : notnull =>
        _lazyRegisterActions.Add(() => RegisterServices(getServicesAction()));

    public ServiceInfoSet GetServices<TServiceCollection>(in TServiceCollection? oneTimeServices = default)
        where TServiceCollection : notnull
    {
        ExecuteLazyRegisterActions();

        return ExtractServiceInfoAndCombineWithCachedServices(oneTimeServices);
    }

    public event EventHandlerWithoutParameters? NewServicesRegistered;

    private ServiceInfoSet ExtractServiceInfoAndCombineWithCachedServices<TServiceCollection>(
        TServiceCollection? oneTimeServices) where TServiceCollection : notnull =>
        _services.Concat(serviceInfoExtractorHandler.GetServiceInfo(oneTimeServices));

    private void ExecuteLazyRegisterActions() => _lazyRegisterActions.ForEach(action => action());
}