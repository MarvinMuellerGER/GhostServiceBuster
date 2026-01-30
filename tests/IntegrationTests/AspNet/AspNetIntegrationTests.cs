using GhostServiceBuster.AspNet;
using GhostServiceBuster.Collections;
using GhostServiceBuster.IntegrationTests.Testees;
using Microsoft.AspNetCore.Builder;
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

            _builder.Services.AddTransient<IService1, Service1>();
            _builder.Services.AddTransient<IService2, Service2>();
            _builder.Services.AddTransient<IService3, Service3>();
            _builder.Services.AddTransient<IService4, Service4>();
            _builder.Services.AddTransient<IService5, Service5>();

            _app = _builder.Build();
            _app.MapControllers();

            _app.MapGet("/mini", MinimalApiHandler.Handle);
            _app.MapGet("/mini", async (IService5 service) => await Task.FromResult(service.GetType().Name));

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
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService4));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService5));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestController));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestPageModel));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(MinimalApiHandler));
        }
    }
}