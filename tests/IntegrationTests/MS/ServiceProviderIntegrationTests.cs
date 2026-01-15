using GhostServiceBuster.IntegrationTests.Testees;
using GhostServiceBuster.MS;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.IntegrationTests.MS;

public static class ServiceProviderIntegrationTests
{
    public sealed class FindUnusedServices
    {
        [Fact]
        public void WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IRootServiceUsingService2, RootServiceUsingService2>();
            serviceCollection.AddTransient<IService1, Service1>();
            serviceCollection.AddTransient<IService2, Service2>();
            serviceCollection.AddTransient<IService3, Service3>();
            serviceCollection.AddSingleton<IServiceCollection>(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var serviceUsageVerifier = serviceProvider.CreateServiceUsageVerifier();

            serviceUsageVerifier.RegisterRootServicesFilters(services =>
                services.Where(s => s.ServiceType == typeof(IRootServiceUsingService2)));

            // exclude Service3
            serviceUsageVerifier.RegisterAllServicesFilters(services =>
                services.Where(s => s.ServiceType != typeof(IService3)));

            // Act
            var unusedServices = serviceUsageVerifier.FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IRootServiceUsingService2));
        }
    }
}