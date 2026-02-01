using GhostServiceBuster.IntegrationTests.Testees;
using GhostServiceBuster.MS;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.IntegrationTests.MS;

public static class ServiceCollectionIntegrationTests
{
    private static readonly IServiceCollection ServiceCollection = new ServiceCollection();

    private static readonly IServiceUsageVerifierWithCachedServicesAndFiltersMutable ServiceUsageVerifier =
        ServiceCollection.CreateServiceUsageVerifier();

    public sealed class FindUnusedServices
    {
        [Fact]
        public void WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            ServiceCollection.AddTransient<IRootServiceUsingService2, RootServiceUsingService2>();
            ServiceCollection.AddTransient<IService1, Service1>();
            ServiceCollection.AddTransient<IService2, Service2>();
            ServiceCollection.AddTransient<IService3, Service3>();

            ServiceUsageVerifier.RegisterRootServicesFilters(services =>
                services.Where(s => s.ServiceType == typeof(IRootServiceUsingService2)), useAllServices: true);

            // exclude Service3
            ServiceUsageVerifier.RegisterAllServicesFilters(services =>
                services.Where(s => s.ServiceType != typeof(IService3)));

            // Act
            var unusedServices = ServiceUsageVerifier.FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IRootServiceUsingService2));
        }
    }
}