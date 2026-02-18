using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet;

/// <summary>
/// Provides extensions for ASP.NET Core <see cref="WebApplication"/> integration.
/// </summary>
public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        /// <summary>
        /// Creates a service usage verifier using the provided app and service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>A mutable verifier configured for ASP.NET Core.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifier(
            IServiceCollection services) =>
            Verify.New.ForAspNet(app, services);

        /// <summary>
        /// Creates a service usage verifier without safety checks for the app.
        /// </summary>
        /// <returns>A mutable verifier configured for ASP.NET Core.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe() =>
            Verify.New.ForAspNetUnsafe(app);
    }
}
