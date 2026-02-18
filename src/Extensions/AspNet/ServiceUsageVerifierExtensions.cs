using GhostServiceBuster.AspNet.Utils;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GhostServiceBuster.AspNet;

/// <summary>
/// Provides extensions for configuring verifiers with ASP.NET Core services.
/// </summary>
public static class ServiceUsageVerifierExtensions
{
    extension<TServiceUsageVerifier>(TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable
    {
        /// <summary>
        /// Configures the verifier for an ASP.NET Core application.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <param name="services">The service collection.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNet(
            WebApplication app, IServiceCollection services) =>
            serviceUsageVerifier.ForAspNet(app.Services, services);

        /// <summary>
        /// Configures the verifier for an ASP.NET Core service provider and collection.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="services">The service collection.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNet(
            IServiceProvider serviceProvider, IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(serviceProvider);

        /// <summary>
        /// Configures the verifier for an ASP.NET Core application without safety checks.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNetUnsafe(WebApplication app) =>
            serviceUsageVerifier.ForAspNetUnsafe(app.Services);

        /// <summary>
        /// Configures the verifier for an ASP.NET Core service provider without safety checks.
        /// </summary>
        /// <param name="services">The service provider.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNetUnsafe(IServiceProvider services) =>
            serviceUsageVerifier.ForServiceProviderUnsafe(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(services);

        /// <summary>
        /// Registers common ASP.NET Core entry points as root services.
        /// </summary>
        /// <param name="services">The service provider.</param>
        /// <returns>The configured verifier.</returns>
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable
            RegisterAspNetApplicationEntryPointsAsRootService(IServiceProvider services) =>
            (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)
            serviceUsageVerifier.RegisterControllersAsRootServices(services)
                .RegisterPageModelsAsRootServices(services)
                .RegisterMinimalApiInjectionRootServicesFilter(services)
                .RegisterHostedServiceRootServicesFilter()
                //.RegisterMiddlewaresAsRootServices(services)
                .RegisterEndpointFiltersAsRootServices()
                .RegisterAuthorizationHandlersAsRootServices(services)
                //.RegisterHealthChecksAsRootServices(services)
                .RegisterViewComponentsAsRootServices(services)
                .RegisterTagHelpersAsRootServices(services);

        private IServiceUsageVerifierWithCachedServicesMutable RegisterControllersAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<IActionDescriptorCollectionProvider>()
                    .ActionDescriptors.Items
                    .OfType<ControllerActionDescriptor>()
                    .Select(a => a.ControllerTypeInfo)
                    .Distinct());

        private IServiceUsageVerifierWithCachedServicesMutable RegisterPageModelsAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(PageModel).IsAssignableFrom(t) && !t.IsAbstract));

        private IServiceUsageVerifierWithCachedServicesMutable RegisterMiddlewaresAsRootServices(
            IServiceCollection services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.Select(s => s.ImplementationType)
                    .OfType<Type>()
                    .Where(t => t.GetConstructors()
                        .Any(c => c.GetParameters()
                            .Any(p => p.ParameterType == typeof(RequestDelegate))))
                    .Distinct());

        private IServiceUsageVerifierWithCachedServicesMutable RegisterEndpointFiltersAsRootServices() =>
            serviceUsageVerifier.RegisterRootServices(AspNetTypesProvider.EndpointFilters);

        private IServiceUsageVerifierWithCachedServicesMutable RegisterAuthorizationHandlersAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetServices<IAuthorizationHandler>()
                    .Select(h => h.GetType())
                    .Distinct());

        private IServiceUsageVerifierWithCachedServicesMutable RegisterHealthChecksAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<HealthCheckService>()
                    .GetType().Assembly
                    .GetTypes()
                    .Where(t =>
                        typeof(IHealthCheck).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
                    .Distinct());

        private IServiceUsageVerifierWithCachedServicesMutable RegisterViewComponentsAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(ViewComponent).IsAssignableFrom(t)));

        private IServiceUsageVerifierWithCachedServicesMutable RegisterTagHelpersAsRootServices(
            IServiceProvider services) =>
            serviceUsageVerifier.RegisterRootServices(
                services.GetRequiredService<ApplicationPartManager>()
                    .ApplicationParts
                    .OfType<AssemblyPart>()
                    .SelectMany(p => p.Types)
                    .Where(t => typeof(TagHelper).IsAssignableFrom(t)));
    }
}
