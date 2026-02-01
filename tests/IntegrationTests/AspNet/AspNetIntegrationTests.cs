using GhostServiceBuster.AspNet;
using GhostServiceBuster.Collections;
using GhostServiceBuster.IntegrationTests.Testees;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.IntegrationTests.AspNet;

public static class AspNetIntegrationTests
{
    public sealed class FindUnusedServices : IAsyncLifetime
    {
        private WebApplication _app = null!;
        private WebApplicationBuilder _builder = null!;

        public async Task InitializeAsync()
        {
            _builder = WebApplication.CreateBuilder();

            _builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(TestController).Assembly);

            _builder.Services
                .AddRazorPages()
                .AddApplicationPart(typeof(TestPageModel).Assembly);

            _builder.Services.AddHostedService<TestHostedService>();

            _builder.Services
                .AddTransient<IService1, Service1>()
                .AddTransient<IServiceInjectedIntoController, ServiceInjectedIntoController>()
                .AddTransient<IServiceInjectedIntoPageModel, ServiceInjectedIntoPageModel>()
                .AddTransient<IServiceInjectedIntoHostedService, ServiceInjectedIntoHostedService>()
                .AddTransient<IServiceInjectedIntoMinimalApiHandler, ServiceInjectedIntoMinimalApiHandler>()
                .AddTransient<IServiceInjectedIntoMinimalApiLambda, ServiceInjectedIntoMinimalApiLambda>()
                .AddTransient<IServiceInjectedIntoMinimalApiLambdaFromBody,
                    ServiceInjectedIntoMinimalApiLambdaFromBody>()
                .AddTransient<IServiceInjectedIntoEndpointFilter, ServiceInjectedIntoEndpointFilter>();

            _app = _builder.Build();
            _app.MapControllers();

            _app.MapGet("/mini", TestMinimalApiHandler.Handle).AddEndpointFilter<TestEndpointFilter>();
            _app.MapGet("/mini",
                async (IServiceInjectedIntoMinimalApiLambda service,
                        [FromBody] IServiceInjectedIntoMinimalApiLambdaFromBody fromBody) =>
                    await Task.FromResult(service.GetType().Name));

            await _app.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _app.StopAsync();
            await _app.DisposeAsync();
        }

        [Fact]
        public void WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            var serviceUsageVerifier = _app.CreateServiceUsageVerifier(_builder.Services);

            // Act
            var unusedServices = serviceUsageVerifier.FindUnusedServices();

            // Assert
            AssertServiceUsage(unusedServices);
        }

        [Fact]
        public void WithRealDependencies_UsingOnlyServiceProvider_ReturnsCorrectResult()
        {
            // Arrange
            var serviceUsageVerifier = _app.CreateServiceUsageVerifierUnsafe();

            // Act
            var unusedServices = serviceUsageVerifier.FindUnusedServices();

            // Assert
            AssertServiceUsage(unusedServices);
        }

        private static void AssertServiceUsage(ServiceInfoSet unusedServices)
        {
            unusedServices.Should().HaveCount(2);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IServiceInjectedIntoMinimalApiLambdaFromBody));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoController));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoPageModel));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoMinimalApiHandler));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoMinimalApiLambda));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoHostedService));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IServiceInjectedIntoEndpointFilter));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestController));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestPageModel));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestMinimalApiHandler));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestHostedService));
        }
    }
}