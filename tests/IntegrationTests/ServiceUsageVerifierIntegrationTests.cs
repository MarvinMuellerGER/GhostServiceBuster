using GhostServiceBuster.Collections;
using GhostServiceBuster.Default;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.IntegrationTests;

public static class ServiceUsageVerifierIntegrationTests
{
    private static readonly IServiceUsageVerifier ServiceUsageVerifier = Verify.New.Default();

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
            var rootService = typeof(RootServiceUsingService1);

            var allServices = new List<Type> { service1, service2, rootService };
            var rootServices = new List<Type> { rootService };

            // Act
            var unusedServices =
                ServiceUsageVerifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService1));
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
            var rootServices = new List<Type> { rootService };

            var excludeService3Filter = new ServiceInfoFilter(services =>
                services.Where(s => s.ServiceType != typeof(IService3)));

            // Act
            var unusedServices = ServiceUsageVerifier
                .RegisterAllServicesFilters(excludeService3Filter)
                .RegisterServices(allServices, rootServices)
                .FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(2);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService3));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IRootService));
        }
    }

    #region Test Interfaces and Classes

    private interface IService1;

    private class Service1 : IService1;

    private interface IService2;

    private class Service2 : IService2;

    private interface IService3;

    private class Service3 : IService3;

    private interface IRootService;

    private class RootService : IRootService;

    private interface IRootServiceUsingService1;

#pragma warning disable CS9113 // Parameter is unread.
    private class RootServiceUsingService1(IService1 service1) : IRootServiceUsingService1;
#pragma warning restore CS9113 // Parameter is unread.

    #endregion
}