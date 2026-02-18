using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet;

/// <summary>
/// Provides extensions for ASP.NET Core service provider integration.
/// </summary>
public static class ServiceProviderExtensions
{
    extension(IServiceProvider serviceProvider)
    {
        /// <summary>
        /// Creates a service usage verifier using the provided service provider and service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>A mutable verifier configured for ASP.NET Core.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifier(
            IServiceCollection services) =>
            Verify.New.ForAspNet(serviceProvider, services);

        /// <summary>
        /// Creates a service usage verifier without safety checks for the service provider.
        /// </summary>
        /// <returns>A mutable verifier configured for ASP.NET Core.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe() =>
            Verify.New.ForAspNetUnsafe(serviceProvider);
    }
}
