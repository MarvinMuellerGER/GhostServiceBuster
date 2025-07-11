// ReSharper disable UnusedParameter.Local

using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.UnitTests.Detect;

public static class UnusedServiceDetectorTests
{
    private static readonly UnusedServiceDetector UnusedServiceDetector = new(new ConstructorInjectionDetector());

    public sealed class FindUnusedServices
    {
        [Fact]
        public void WhenNoRootServices_ReturnsAllServices()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(IService1), typeof(Service1Impl));
            var service2 = new ServiceInfo(typeof(IService2), typeof(Service2Impl));

            var allServices = new ServiceInfoSet([service1, service2]);
            var rootServices = ServiceInfoSet.Empty;

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().BeEquivalentTo(allServices);
        }

        [Fact]
        public void WhenAllServicesAreRoots_ReturnsEmptySet()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(IService1), typeof(Service1Impl));
            var service2 = new ServiceInfo(typeof(IService2), typeof(Service2Impl));

            var allServices = new ServiceInfoSet([service1, service2]);
            var rootServices = allServices;

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().BeEmpty();
        }

        [Fact]
        public void WhenDirectDependency_FindsUsedServices()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(IService1), typeof(Service1Impl));
            var service2 = new ServiceInfo(typeof(IService2), typeof(Service2WithDependencyImpl));
            var service3 = new ServiceInfo(typeof(IService3), typeof(Service3Impl));

            var allServices = new ServiceInfoSet([service1, service2, service3]);
            var rootServices = new ServiceInfoSet([service2]); // Service2 is the root and depends on Service1

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(service3); // Only Service3 is unused
        }

        [Fact]
        public void WithTransitiveDependencies_FindsAllUsedServices()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(IService1), typeof(Service1Impl));
            var service2 =
                new ServiceInfo(typeof(IService2), typeof(Service2WithDependencyImpl)); // Depends on Service1
            var service3 =
                new ServiceInfo(typeof(IService3), typeof(Service3WithDependencyImpl)); // Depends on Service2
            var service4 = new ServiceInfo(typeof(IService4), typeof(Service4Impl));

            var allServices = new ServiceInfoSet([service1, service2, service3, service4]);
            var rootServices = new ServiceInfoSet([service3]); // Service3 is the root

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(service4); // Only Service4 is unused
        }

        [Fact]
        public void WithCyclicDependencies_HandlesThemCorrectly()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(ICyclicService1), typeof(CyclicService1Impl)); // Depends on Service2
            var service2 = new ServiceInfo(typeof(ICyclicService2), typeof(CyclicService2Impl)); // Depends on Service1
            var service3 = new ServiceInfo(typeof(IService3), typeof(Service3Impl));

            var allServices = new ServiceInfoSet([service1, service2, service3]);
            var rootServices = new ServiceInfoSet([service1]); // Service1 is the root

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(service3); // Only Service3 is unused
        }

        [Fact]
        public void WithMultipleRoots_FindsAllUsedServices()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var service1 = new ServiceInfo(typeof(IService1), typeof(Service1Impl));
            var service2 =
                new ServiceInfo(typeof(IService2), typeof(Service2WithDependencyImpl)); // Depends on Service1
            var service3 =
                new ServiceInfo(typeof(IService3), typeof(Service3WithService4DependencyImpl)); // Depends on Service4
            var service4 = new ServiceInfo(typeof(IService4), typeof(Service4Impl));
            var service5 = new ServiceInfo(typeof(IService5), typeof(Service5Impl));

            var allServices = new ServiceInfoSet([service1, service2, service3, service4, service5]);
            var rootServices = new ServiceInfoSet([service2, service3]); // Multiple roots

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(service5); // Only Service5 is unused
        }

        [Fact]
        public void WithGenericParameters_HandlesThemCorrectly()
        {
            // Arrange
            var verifier = UnusedServiceDetector;

            var genericService = new ServiceInfo(typeof(IGenericService<>), typeof(GenericServiceImpl<>));
            var genericConsumer = new ServiceInfo(typeof(IGenericConsumer),
                typeof(GenericConsumerImpl)); // Depends on IGenericService<string>
            var unusedService = new ServiceInfo(typeof(IService1), typeof(Service1Impl));

            var allServices = new ServiceInfoSet([genericService, genericConsumer, unusedService]);
            var rootServices = new ServiceInfoSet([genericConsumer]);

            // Act
            var unusedServices = verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(unusedService);
        }
    }

    #region Test Service Interfaces

    private interface IService1;

    private interface IService2;

    private interface IService3;

    private interface IService4;

    private interface IService5;

    private interface ICyclicService1;

    private interface ICyclicService2;

    // ReSharper disable once UnusedTypeParameter
    private interface IGenericService<T>;

    private interface IGenericConsumer;

    #endregion

    #region Test Service Implementations

    private class Service1Impl : IService1;

    private class Service2Impl : IService2;

    private class Service2WithDependencyImpl : IService2
    {
        public Service2WithDependencyImpl(IService1 service1)
        {
        }
    }

    private class Service3Impl : IService3;

    private class Service3WithDependencyImpl : IService3
    {
        public Service3WithDependencyImpl(IService2 service2)
        {
        }
    }

    private class Service3WithService4DependencyImpl : IService3
    {
        public Service3WithService4DependencyImpl(IService4 service4)
        {
        }
    }

    private class Service4Impl : IService4;

    private class Service5Impl : IService5;

    private class CyclicService1Impl : ICyclicService1
    {
        public CyclicService1Impl(ICyclicService2 service2)
        {
        }
    }

    private class CyclicService2Impl : ICyclicService2
    {
        public CyclicService2Impl(ICyclicService1 service1)
        {
        }
    }

    private class GenericServiceImpl<T> : IGenericService<T>;

    private class GenericConsumerImpl : IGenericConsumer
    {
        public GenericConsumerImpl(IGenericService<string> genericService)
        {
        }
    }

    #endregion
}