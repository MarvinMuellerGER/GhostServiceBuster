namespace GhostServiceBuster.NServiceBus;

/// <summary>
/// Provides extensions for NServiceBus service provider integration.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Creates a service usage verifier without safety checks for the provided service provider.
    /// </summary>
    /// <param name="services">The service provider.</param>
    /// <returns>A mutable verifier configured for NServiceBus.</returns>
    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        CreateServiceUsageVerifierForNServiceBusUnsafe(this IServiceProvider services) =>
        Verify.New.ForNServiceBusUnsafe(services);
}
