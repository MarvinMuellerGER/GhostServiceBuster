using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;

namespace GhostServiceBuster.Cache;

internal sealed class ServiceCacheHandler(IServiceInfoExtractorHandler serviceInfoExtractorHandler)
    : IServiceCacheHandler
{
    private ServiceInfoSet _services = [];

    public void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull
    {
        _services = [];
        RegisterServices(services);
    }

    public void RegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull
    {
        _services = GetServices(services);
        NewServicesRegistered?.Invoke();
    }

    public ServiceInfoSet GetServices<TServiceCollection>(in TServiceCollection? oneTimeServices = default)
        where TServiceCollection : notnull =>
        _services.Concat(serviceInfoExtractorHandler.GetServiceInfo(oneTimeServices));

    public event EventHandlerWithoutParameters? NewServicesRegistered;
}