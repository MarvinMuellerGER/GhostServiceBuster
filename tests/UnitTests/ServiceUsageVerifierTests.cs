using System.Collections;
using GhostServiceBuster.Cache;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;
using NSubstitute;

namespace GhostServiceBuster.UnitTests;

public static class ServiceUsageVerifierTests
{
    private static readonly IUnusedServiceDetector UnusedServiceDetector =
        Substitute.For<IUnusedServiceDetector>();

    private static readonly IServiceInfoExtractorHandler ServiceInfoExtractorHandler =
        Substitute.For<IServiceInfoExtractorHandler>();

    private static readonly IFilterHandler FilterHandler = Substitute.For<IFilterHandler>();

    private static readonly IRootFilterHandler RootFilterHandler = Substitute.For<IRootFilterHandler>();

    private static readonly IServiceCacheHandler AllServicesCacheHandler =
        Substitute.For<IServiceCacheHandler>();

    private static readonly IServiceCacheHandler RootServicesCacheHandler =
        Substitute.For<IServiceCacheHandler>();

    private static readonly IServiceCacheHandler UnusedServicesCacheHandler =
        Substitute.For<IServiceCacheHandler>();

    private static readonly IFilterCacheHandler AllServicesFilterCacheHandler = Substitute.For<IFilterCacheHandler>();

    private static readonly IRootFilterCacheHandler RootServicesFilterCacheHandler =
        Substitute.For<IRootFilterCacheHandler>();

    private static readonly IFilterCacheHandler UnusedServicesFilterCacheHandler
        = Substitute.For<IFilterCacheHandler>();

    private static readonly IServiceAndFilterCacheHandler AllServicesAndFilterCacheHandler =
        Substitute.For<IServiceAndFilterCacheHandler>();

    private static readonly IRootServiceAndFilterCacheHandler RootServicesAndFilterCacheHandler =
        Substitute.For<IRootServiceAndFilterCacheHandler>();

    private static readonly IServiceAndFilterCacheHandler UnusedServicesAndFilterCacheHandler =
        Substitute.For<IServiceAndFilterCacheHandler>();

    private static readonly IServiceUsageVerifier Verifier
        = new ServiceUsageVerifier(UnusedServiceDetector, ServiceInfoExtractorHandler, FilterHandler, RootFilterHandler,
            AllServicesCacheHandler, RootServicesCacheHandler, UnusedServicesCacheHandler,
            AllServicesFilterCacheHandler, RootServicesFilterCacheHandler, UnusedServicesFilterCacheHandler,
            AllServicesAndFilterCacheHandler, RootServicesAndFilterCacheHandler, UnusedServicesAndFilterCacheHandler);

    private static readonly ServiceInfo ServiceInfo1 = new(typeof(IService1), typeof(Service1));
    private static readonly ServiceInfo ServiceInfo2 = new(typeof(IService2), typeof(Service2));
    private static readonly ServiceInfo ServiceInfo3 = new(typeof(IService3), typeof(Service3));

    private static readonly ServiceInfoSet ServiceInfoSet = new(ServiceInfo1, ServiceInfo2, ServiceInfo3);

    private static readonly ServiceInfoFilter Service1Filter = services => services.Where(s => s == ServiceInfo1);

    private static readonly ServiceInfoFilter Service1Or2Filter =
        services => services.Where(s => s == ServiceInfo1 || s == ServiceInfo2);

    private static readonly ServiceInfoFilter Service2Filter = services => services.Where(s => s == ServiceInfo2);

    static ServiceUsageVerifierTests()
    {
        FilterHandler.ApplyFilters(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));


        AllServicesFilterCacheHandler.ApplyFilters(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));

        RootServicesFilterCacheHandler.ApplyFilters(
                Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));

        UnusedServicesFilterCacheHandler.ApplyFilters(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));


        AllServicesAndFilterCacheHandler
            .GetFilteredServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));

        RootServicesAndFilterCacheHandler
            .GetFilteredServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));

        UnusedServicesAndFilterCacheHandler
            .GetFilteredServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
            .Returns(args => args.ArgAt<ServiceInfoSet>(0));
    }

    public sealed class RegisterServiceInfoExtractor
    {
        [Fact]
        public void DelegatesCallToHandler()
        {
            // Arrange
            ServiceInfoExtractor<List<string>> extractor = _ => ServiceInfoSet;

            // Act
            Verifier.RegisterServiceInfoExtractor(extractor);

            // Assert
            ServiceInfoExtractorHandler.Received(1).RegisterServiceInfoExtractor(extractor);
        }
    }

    public sealed class FindUnusedServicesUsingOnlyOneTimeFilters
    {
        [Fact]
        public void WithDifferentCollectionTypes_WorksCorrectly()
        {
            // Arrange
            var allServices = new List<Type> { typeof(Service1), typeof(Service2), typeof(Service3) };
            var rootServices = new HashSet<Type> { typeof(Service2) };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2, ServiceInfo3]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo3]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo3);
        }

        [Fact]
        public void WithCustomServiceCollection_WorksCorrectly()
        {
            // Arrange
            var allServices = new CustomServiceCollection(typeof(Service1), typeof(Service2), typeof(Service3));
            var rootServices = new List<Type> { typeof(Service3) };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2, ServiceInfo3]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo3]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo2);
        }


        [Fact]
        public void CallsExtractorForBothCollections()
        {
            // Arrange
            var allServices = new List<string> { "Service1" };
            var rootServices = new List<string> { "Service2" };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(new ServiceInfoSet([ServiceInfo1]));

            // Act
            var result = Verifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }


        [Fact]
        public void DelegatesFilteringCorrectly()
        {
            // Arrange
            var allServices = new List<string> { "Service1", "Service2" };
            var rootServices = new List<string> { "Service2" };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1]);

            var allServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service2Filter)
            );

            var unusedServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            FilterHandler.ApplyFilters(extractedAllServices, allServicesFilters).Returns(extractedAllServices);
            FilterHandler.ApplyFilters(extractedRootServices, rootServicesFilters).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            FilterHandler.ApplyFilters(unusedServices, unusedServicesFilters).Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
                allServices,
                rootServices,
                allServicesFilters,
                rootServicesFilters,
                unusedServicesFilters);

            // Assert
            FilterHandler.Received(1).ApplyFilters(extractedAllServices, allServicesFilters);
            FilterHandler.Received(1).ApplyFilters(extractedRootServices, rootServicesFilters);
            FilterHandler.Received(1).ApplyFilters(unusedServices, unusedServicesFilters);

            result.Should().BeSameAs(unusedServices);
        }


        [Fact]
        public void WithSameCollectionForAllAndRoot_WorksCorrectly()
        {
            // Arrange
            var services = new List<string> { "Service1", "Service2" };

            var extractedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);

            var allServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service2Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(services).Returns(extractedServices);

            FilterHandler.ApplyFilters(extractedServices, allServicesFilters).Returns(extractedServices);
            FilterHandler.ApplyFilters(extractedServices, rootServicesFilters)
                .Returns(new ServiceInfoSet([ServiceInfo2]));

            UnusedServiceDetector.FindUnusedServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoSet>())
                .Returns([]);

            // Act
            var result = Verifier.FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
                services,
                services,
                allServicesFilters,
                rootServicesFilters);

            // Assert
            ServiceInfoExtractorHandler.Received(2).GetServiceInfo(services);
            FilterHandler.Received(1).ApplyFilters(extractedServices, allServicesFilters);
            FilterHandler.Received(1).ApplyFilters(extractedServices, rootServicesFilters);
            result.Should().BeEmpty();
        }
    }

    public sealed class FindUnusedServices
    {
        [Fact]
        public void WithDifferentCollectionTypes_WorksCorrectly()
        {
            // Arrange
            var allServices = new List<Type> { typeof(Service1), typeof(Service2), typeof(Service3) };
            var rootServices = new HashSet<Type> { typeof(Service2) };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2, ServiceInfo3]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo3]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo3);
        }

        [Fact]
        public void WithCustomServiceCollection_WorksCorrectly()
        {
            // Arrange
            var allServices = new CustomServiceCollection(typeof(Service1), typeof(Service2), typeof(Service3));
            var rootServices = new List<Type> { typeof(Service3) };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2, ServiceInfo3]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo3]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo2);
        }


        [Fact]
        public void CallsExtractorForBothCollections()
        {
            // Arrange
            var allServices = new List<string> { "Service1", "Service2" };
            var rootServices = new List<string> { "Service2" };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1]);

            AllServicesAndFilterCacheHandler.GetFilteredServices(allServices).Returns(extractedAllServices);
            RootServicesAndFilterCacheHandler.GetFilteredServices(rootServices).Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            UnusedServicesAndFilterCacheHandler.GetFilteredServices().Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServices(allServices, rootServices);

            // Assert
            AllServicesAndFilterCacheHandler.Received(1).GetFilteredServices(allServices);
            RootServicesAndFilterCacheHandler.Received(1).GetFilteredServices(rootServices);
            UnusedServicesCacheHandler.Received(1).ClearAndRegisterServices(unusedServices);
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }


        [Fact]
        public void DelegatesFilteringCorrectly()
        {
            // Arrange
            var allServices = new List<string> { "Service1", "Service2" };
            var rootServices = new List<string> { "Service2" };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1]);

            var allServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service2Filter)
            );

            var unusedServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            AllServicesFilterCacheHandler.ApplyFilters(extractedAllServices, allServicesFilters)
                .Returns(extractedAllServices);

            RootServicesFilterCacheHandler
                .ApplyFilters(extractedAllServices, extractedRootServices, rootServicesFilters)
                .Returns(extractedRootServices);

            UnusedServiceDetector.FindUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            UnusedServicesFilterCacheHandler.ApplyFilters(unusedServices, unusedServicesFilters)
                .Returns(unusedServices);

            // Act
            var result = Verifier.FindUnusedServices(
                allServices,
                rootServices,
                allServicesFilters,
                rootServicesFilters,
                unusedServicesFilters);

            // Assert
            AllServicesFilterCacheHandler.Received(1).ApplyFilters(extractedAllServices, allServicesFilters);
            RootServicesFilterCacheHandler.Received(1)
                .ApplyFilters(extractedAllServices, extractedRootServices, rootServicesFilters);

            UnusedServicesFilterCacheHandler.Received(1).ApplyFilters(unusedServices, unusedServicesFilters);

            result.Should().BeSameAs(unusedServices);
        }


        [Fact]
        public void WithSameCollectionForAllAndRoot_WorksCorrectly()
        {
            // Arrange
            var services = new List<string> { "Service1", "Service2" };

            var extractedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);

            var allServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service2Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(services).Returns(extractedServices);

            AllServicesFilterCacheHandler.ApplyFilters(extractedServices, allServicesFilters)
                .Returns(extractedServices);

            RootServicesFilterCacheHandler.ApplyFilters(extractedServices, [], rootServicesFilters)
                .Returns(new ServiceInfoSet([ServiceInfo2]));

            UnusedServiceDetector.FindUnusedServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoSet>())
                .Returns([]);

            // Act
            var result = Verifier.FindUnusedServices(
                services,
                services,
                allServicesFilters,
                rootServicesFilters);

            // Assert
            ServiceInfoExtractorHandler.Received(2).GetServiceInfo(services);
            AllServicesFilterCacheHandler.Received(1).ApplyFilters(extractedServices, allServicesFilters);
            RootServicesFilterCacheHandler.Received(1).ApplyFilters(extractedServices, [], rootServicesFilters);
            result.Should().BeEmpty();
        }
    }

    private class CustomServiceCollection(params Type[] types) : IEnumerable<Type>
    {
        public IEnumerator<Type> GetEnumerator() => ((IEnumerable<Type>)types).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => types.GetEnumerator();
    }

    #region Test Interfaces and Classes

    private interface IService1;

    private interface IService2;

    private interface IService3;

    private class Service1 : IService1;

    private class Service2 : IService2;

    private class Service3 : IService3;

    #endregion
}