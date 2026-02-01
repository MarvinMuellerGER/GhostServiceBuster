using GhostServiceBuster.IntegrationTests.Testees;
using GhostServiceBuster.NServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.IntegrationTests.NSB;

public static class NServiceBusMessageHandlerIntegrationTests
{
    private static readonly IServiceCollection ServiceCollection = new ServiceCollection();

    private static readonly IServiceUsageVerifierWithCachedServicesAndFiltersMutable ServiceUsageVerifier =
        ServiceCollection.CreateServiceUsageVerifierForNServiceBus();

    public sealed class FindUnusedServices
    {
        [Fact]
        public void WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            ServiceCollection.AddTransient<MessageHandlerUsingService2>();
            ServiceCollection.AddTransient<IService1, Service1>();
            ServiceCollection.AddTransient<IService2, Service2>();

            // Act
            var unusedServices = ServiceUsageVerifier.FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(MessageHandlerUsingService2));
        }
    }
}