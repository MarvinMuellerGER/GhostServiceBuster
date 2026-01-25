using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public IServiceUsageVerifier CreateServiceUsageVerifier(IServiceCollection services) =>
            Verify.New.ForAspNet(app, services);

        public IServiceUsageVerifier CreateServiceUsageVerifierUnsafe() =>
            Verify.New.ForAspNetUnsafe(app);
    }
}