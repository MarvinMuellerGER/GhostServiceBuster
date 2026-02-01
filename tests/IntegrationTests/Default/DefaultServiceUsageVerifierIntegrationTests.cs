using GhostServiceBuster.Collections;
using GhostServiceBuster.Default;
using GhostServiceBuster.Filter;
using GhostServiceBuster.IntegrationTests.Testees;

namespace GhostServiceBuster.IntegrationTests.Default;

public static class DefaultServiceUsageVerifierIntegrationTests
{
    private static readonly IServiceUsageVerifierWithoutCachesMutable ServiceUsageVerifier = Verify.New.Default();

    public sealed class FindUnusedServicesUsingOnlyOneTimeFilters
    {
        [Fact]
        public void WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            var service1 = typeof(Service1);
            var service2 = typeof(Service2);
            var service3 = typeof(Service3);
            var rootService = typeof(RootService);

            var allServices = new HashSet<Type> { service1, service2, service3, rootService };
            var rootServices = new List<Type> { rootService };

            var excludeService3Filter =
                new ServiceInfoFilter(services => services.Where(s => s.ServiceType != typeof(IService3)));

            var allServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(excludeService3Filter)
            );

            // Act
            var unusedServices =
                ServiceUsageVerifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices,
                    allServicesFilters);

            // Assert
            unusedServices.Should().HaveCount(2);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IRootService));
        }

        [Fact]
        public void WithRootServiceUsingOtherService_DoesNotIncludeUsedService()
        {
            // Arrange
            var service1 = typeof(Service1);
            var service2 = typeof(Service2);
            var rootService = typeof(RootServiceUsingService2);

            var allServices = new List<Type> { service1, service2, rootService };
            var rootServices = new List<Type> { rootService };

            // Act
            var unusedServices =
                ServiceUsageVerifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
        }
    }

    public sealed class FindUnusedServices
    {
        [Fact]
        public void WithRegisterAllServicesFiltersCallBefore_ReturnsCorrectResult()
        {
            // Arrange
            var service1 = typeof(Service1);
            var service2 = typeof(Service2);
            var service3 = typeof(Service3);
            var rootService = typeof(RootService);

            Type[] allServices = [service1, service2, service3, rootService];

            var excludeService3Filter = new ServiceInfoFilter(services =>
                services.Where(s => s.ServiceType != typeof(IService3)));

            // Act
            var unusedServices = ServiceUsageVerifier
                .RegisterAllServicesFilters(excludeService3Filter)
                .RegisterServices(allServices, rootService)
                .FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(2);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IRootService));
        }
    }
}