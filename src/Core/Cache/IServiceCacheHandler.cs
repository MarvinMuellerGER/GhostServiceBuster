using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal interface IServiceCacheHandler
{
    void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull;

    void RegisterServices<TServiceCollection>(in TServiceCollection services) where TServiceCollection : notnull;

    ServiceInfoSet GetServices<TServiceCollection>(in TServiceCollection? oneTimeServices = default)
        where TServiceCollection : notnull;

    ServiceInfoSet GetServices() => GetServices<object>();

    event EventHandlerWithoutParameters NewServicesRegistered;
}