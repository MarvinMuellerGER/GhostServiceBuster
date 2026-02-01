using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifier(
            IServiceCollection services) =>
            Verify.New.ForAspNet(app, services);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable CreateServiceUsageVerifierUnsafe() =>
            Verify.New.ForAspNetUnsafe(app);
    }
}