namespace GhostServiceBuster.MS;

/// <summary>
/// Provides extensions for <see cref="IServiceProvider"/> integration.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Creates a service usage verifier without safety checks for the provided service provider.
    /// </summary>
    /// <param name="services">The service provider.</param>
    /// <returns>A mutable verifier configured for the service provider.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe(
        this IServiceProvider services) =>
        Verify.New.ForServiceProviderUnsafe(services);
}
