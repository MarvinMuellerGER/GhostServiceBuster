using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

internal sealed class ServiceInfoExtractorHandler : IServiceInfoExtractorHandler
{
    private readonly Dictionary<Type, ServiceInfoExtractorInternal> _serviceInfoExtractors = [];

    public void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        if (!TryAddNewServiceInfoExtractor(extractor))
            throw new InvalidOperationException(
                $"A service info extractor for {typeof(TServiceCollection).FullName} is already registered.");
    }

    public ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection)
        where TServiceCollection : notnull
    {
        if (serviceCollection is ServiceInfoSet serviceInfoSet)
            return serviceInfoSet;

        var serviceCollectionType = typeof(TServiceCollection);
        _ = _serviceInfoExtractors.TryGetValue(serviceCollectionType, out var extractor);

        return extractor?.Invoke(serviceCollection)
               ?? throw new InvalidOperationException(
                   $"No service info extractor registered for {serviceCollectionType.FullName}.");
    }

    private bool TryAddNewServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        return _serviceInfoExtractors.TryAdd(typeof(TServiceCollection),
            serviceCollection => extractor((TServiceCollection)serviceCollection));
    }

    private delegate ServiceInfoSet ServiceInfoExtractorInternal(object serviceCollection);
}