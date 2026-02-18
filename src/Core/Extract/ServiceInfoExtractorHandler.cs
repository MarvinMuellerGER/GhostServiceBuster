using System.Collections;
using System.Collections.Immutable;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Extract;

/// <summary>
/// Manages registration and selection of service info extractors.
/// </summary>
internal sealed class ServiceInfoExtractorHandler : IServiceInfoExtractorHandler
{
    private readonly Dictionary<Type, ServiceInfoExtractorInternal> _serviceInfoExtractors = [];

    /// <summary>
    /// Registers a service info extractor for a collection type.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    public void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        if (!TryAddNewServiceInfoExtractor(extractor))
            throw new InvalidOperationException(
                $"A service info extractor for {typeof(TServiceCollection).FullName} is already registered.");
    }

    /// <summary>
    /// Extracts service information from the provided collection.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="serviceCollection">The collection to inspect.</param>
    /// <returns>The extracted services.</returns>
    public ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection)
    {
        switch (serviceCollection)
        {
            case null:
                return [];

            case ServiceInfoSet serviceInfoSet:
                return serviceInfoSet;

            case IEnumerable<ServiceInfo> serviceInfoEnumerable:
                return serviceInfoEnumerable.ToImmutableHashSet();
        }

        var extractor = DetermineExtractor(serviceCollection, out bool extractorIsForEnumerable);

        if (serviceCollection is not IEnumerable serviceCollectionAsEnumerable || extractorIsForEnumerable)
            return extractor(serviceCollection);

        return serviceCollectionAsEnumerable.Select(service => extractor(service!).First());
    }

    private ServiceInfoExtractorInternal DetermineExtractor<TServiceCollection>(
        TServiceCollection serviceCollection, out bool extractorIsForEnumerable)
    {
        var collectionType = typeof(TServiceCollection);
        var serviceCollectionType = serviceCollection is IEnumerable and not string
            ? GetElementType(collectionType)
            : collectionType;

        var foundExtractor = TryGetExtractor(serviceCollectionType, out var extractor);
        extractorIsForEnumerable = false;
        if (!foundExtractor && serviceCollection is IEnumerable)
        {
            foundExtractor = extractorIsForEnumerable =
                TryGetExtractor(collectionType, out extractor);

            if (!foundExtractor)
            {
                var serviceEnumerableType = typeof(IEnumerable<>).MakeGenericType(serviceCollectionType);
                foundExtractor = extractorIsForEnumerable =
                    TryGetExtractor(serviceEnumerableType, out extractor);
            }
        }

        if (!foundExtractor || extractor is null)
            throw new InvalidOperationException(
                $"No service info extractor registered for {serviceCollectionType.FullName}.");

        return extractor;

        bool TryGetExtractor(Type type, out ServiceInfoExtractorInternal? serviceInfoExtractorInternal)
        {
            if (_serviceInfoExtractors.TryGetValue(type, out serviceInfoExtractorInternal))
                return true;

            serviceInfoExtractorInternal =
                _serviceInfoExtractors.FirstOrDefault(kvp => kvp.Key.IsAssignableFrom(type)).Value;

            return serviceInfoExtractorInternal is not null;
        }
    }

    private bool TryAddNewServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        return _serviceInfoExtractors.TryAdd(typeof(TServiceCollection),
            serviceCollection => extractor((TServiceCollection)serviceCollection));
    }

    private static Type GetElementType(Type type)
    {
        // Type is Array
        if (type.IsArray)
            return type.GetElementType()!;

        // Type is IEnumerable<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];

        // Type implements/extends IEnumerable<T>
        return type.GetInterfaces()
                   .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                   .Select(t => t.GenericTypeArguments[0]).FirstOrDefault()
               // Type is not a Enumerable
               ?? type;
    }

    private delegate ServiceInfoSet ServiceInfoExtractorInternal(object serviceCollection);
}
