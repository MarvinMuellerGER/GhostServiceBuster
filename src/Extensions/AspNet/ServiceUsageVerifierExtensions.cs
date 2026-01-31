using GhostServiceBuster.AspNet.Filter;
using GhostServiceBuster.MS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GhostServiceBuster.AspNet;

public static class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifier serviceUsageVerifier)
    {
        public IServiceUsageVerifier ForAspNet(WebApplication app, IServiceCollection services) =>
            serviceUsageVerifier.ForAspNet(app.Services, services);

        public IServiceUsageVerifier ForAspNet(IServiceProvider serviceProvider, IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(serviceProvider);

        public IServiceUsageVerifier ForAspNetUnsafe(WebApplication app) =>
            serviceUsageVerifier.ForAspNetUnsafe(app.Services);

        public IServiceUsageVerifier ForAspNetUnsafe(IServiceProvider services) =>
            serviceUsageVerifier.ForServiceProviderUnsafe(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(services);

        public IServiceUsageVerifier RegisterAspNetApplicationEntryPointsAsRootService(IServiceProvider services) =>
            serviceUsageVerifier.RegisterControllersAsRootServices(services)
                .RegisterPageModelsAsRootServices(services)
                .RegisterMinimalApiInjectionRootServicesFilter(services)
                .RegisterHostedServiceRootServicesFilter()
                //.RegisterMiddlewaresAsRootServices(services)
                .RegisterEndpointFiltersAsRootServices(services)
                .RegisterAuthorizationHandlersAsRootServices(services)
                //.RegisterHealthChecksAsRootServices(services)
                .RegisterViewComponentsAsRootServices(services)
                .RegisterTagHelpersAsRootServices(services);

        private IServiceUsageVerifier RegisterControllersAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<IActionDescriptorCollectionProvider>()
                    .ActionDescriptors.Items
                    .OfType<ControllerActionDescriptor>()
                    .Select(a => a.ControllerTypeInfo)
                    .Distinct());

        private IServiceUsageVerifier RegisterPageModelsAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(PageModel).IsAssignableFrom(t) && !t.IsAbstract));

        private IServiceUsageVerifier RegisterMiddlewaresAsRootServices(IServiceCollection services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.Select(s => s.ImplementationType)
                    .OfType<Type>()
                    .Where(t => t.GetConstructors()
                        .Any(c => c.GetParameters()
                            .Any(p => p.ParameterType == typeof(RequestDelegate))))
                    .Distinct());

        private IServiceUsageVerifier RegisterEndpointFiltersAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<EndpointDataSource>()
                    .Endpoints
                    .SelectMany(e => e.Metadata.OfType<IEndpointFilter>())
                    .Select(f => f.GetType())
                    .Distinct());

        private IServiceUsageVerifier RegisterAuthorizationHandlersAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetServices<IAuthorizationHandler>()
                    .Select(h => h.GetType())
                    .Distinct());

        private IServiceUsageVerifier RegisterHealthChecksAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<HealthCheckService>()
                    .GetType().Assembly
                    .GetTypes()
                    .Where(t =>
                        typeof(IHealthCheck).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
                    .Distinct());

        private IServiceUsageVerifier RegisterViewComponentsAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(ViewComponent).IsAssignableFrom(t)));

        private IServiceUsageVerifier RegisterTagHelpersAsRootServices(IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(TagHelper).IsAssignableFrom(t)));
    }
}