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

public static class ServiceUsageVerifierExtensions
{
    extension<TServiceUsageVerifier>(TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable
    {
        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNet(
            WebApplication app, IServiceCollection services) =>
            serviceUsageVerifier.ForAspNet(app.Services, services);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNet(
            IServiceProvider serviceProvider, IServiceCollection services) =>
            serviceUsageVerifier.ForServiceCollection(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(serviceProvider);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNetUnsafe(WebApplication app) =>
            serviceUsageVerifier.ForAspNetUnsafe(app.Services);

        public IServiceUsageVerifierWithCachedServicesAndFiltersMutable ForAspNetUnsafe(IServiceProvider services) =>
            serviceUsageVerifier.ForServiceProviderUnsafe(services)
                .RegisterAspNetApplicationEntryPointsAsRootService(services);

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