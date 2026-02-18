using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.NServiceBus;

/// <summary>
/// Provides extensions for NServiceBus service collection integration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Creates a service usage verifier for the provided service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>A mutable verifier configured for NServiceBus.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierForNServiceBus(
        this IServiceCollection services) =>
        Verify.New.ForNServiceBus(services);
}
