using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

/// <summary>
/// Provides extensions for <see cref="IServiceCollection"/> integration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Creates a service usage verifier for the provided service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>A mutable verifier configured for the service collection.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifier(
        this IServiceCollection services) =>
        Verify.New.ForServiceCollection(services);
}
