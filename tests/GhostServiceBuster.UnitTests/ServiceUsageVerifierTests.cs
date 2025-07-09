using System.Collections.Immutable;
using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;
using GhostServiceBuster.ServiceInfoExtractor;
using NSubstitute;

namespace GhostServiceBuster.UnitTests;

public static class ServiceUsageVerifierTests
{
    private static readonly ICoreServiceUsageVerifier CoreServiceUsageVerifier =
        Substitute.For<ICoreServiceUsageVerifier>();

    private static readonly IServiceInfoExtractorHandler ServiceInfoExtractorHandler =
        Substitute.For<IServiceInfoExtractorHandler>();

    private static readonly IFilterHandler FilterHandler = Substitute.For<IFilterHandler>();

    private static readonly ServiceUsageVerifier Verifier
        = new(CoreServiceUsageVerifier, ServiceInfoExtractorHandler, FilterHandler);

    private static readonly ServiceInfo ServiceInfo1 = new(typeof(IService1), typeof(Service1));
    private static readonly ServiceInfo ServiceInfo2 = new(typeof(IService2), typeof(Service2));
    private static readonly ServiceInfo ServiceInfo3 = new(typeof(IService3), typeof(Service3));

    private static readonly ServiceInfoSet ServiceInfoSet = new(ServiceInfo1, ServiceInfo2, ServiceInfo3);

    private static readonly ServiceInfoFilter Service1Filter = services =>
        services.Where(s => s == ServiceInfo1).ToImmutableHashSet();

    private static readonly ServiceInfoFilter Service1Or2Filter = services =>
        services.Where(s => s == ServiceInfo1 || s == ServiceInfo2).ToImmutableHashSet();

    private static readonly ServiceInfoFilter Service2Filter = services =>
        services.Where(s => s == ServiceInfo2).ToImmutableHashSet();

    private static readonly ServiceInfoFilter Service2Or3Filter = services =>
        services.Where(s => s == ServiceInfo2 || s == ServiceInfo3).ToImmutableHashSet();

    private static readonly ServiceInfoFilter Service3Filter = services =>
        services.Where(s => s == ServiceInfo3).ToImmutableHashSet();

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

    public sealed class GetUnusedServices
    {
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

            FilterHandler.ApplyFilters(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoFilterInfoList>())
                .Returns(args => args.ArgAt<ServiceInfoSet>(0));

            CoreServiceUsageVerifier.GetUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(new ServiceInfoSet([ServiceInfo1]));

            // Act
            var result = Verifier.GetUnusedServices(allServices, rootServices);

            // Assert
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(allServices);
            ServiceInfoExtractorHandler.Received(1).GetServiceInfo(rootServices);
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }

        [Fact]
        public void AppliesFiltersToAllCollections()
        {
            // Arrange
            var allServices = new List<string> { "Service1", "Service2", "Service3" };
            var rootServices = new List<string> { "Service2" };

            var extractedAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2, ServiceInfo3]);
            var extractedRootServices = new ServiceInfoSet([ServiceInfo2]);
            var unusedServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo3]);

            // Verwende die vordefinierten Filter in den Filter-Listen
            var allServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Or2Filter, true)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            var unusedServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Filter, true)
            );

            var filteredAllServices = new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);
            var filteredRootServices = new ServiceInfoSet([ServiceInfo2]);
            var filteredUnusedServices = new ServiceInfoSet([ServiceInfo1]);

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            FilterHandler.ApplyFilters(extractedAllServices, allServicesFilters).Returns(filteredAllServices);
            FilterHandler.ApplyFilters(extractedRootServices, rootServicesFilters).Returns(filteredRootServices);

            CoreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices)
                .Returns(unusedServices);

            FilterHandler.ApplyFilters(unusedServices, unusedServicesFilters).Returns(filteredUnusedServices);

            // Act
            var result = Verifier.GetUnusedServices(
                allServices,
                rootServices,
                allServicesFilters,
                rootServicesFilters,
                unusedServicesFilters);

            // Assert
            FilterHandler.Received(1).ApplyFilters(extractedAllServices, allServicesFilters);
            FilterHandler.Received(1).ApplyFilters(extractedRootServices, rootServicesFilters);
            FilterHandler.Received(1).ApplyFilters(unusedServices, unusedServicesFilters);

            result.Should().BeSameAs(filteredUnusedServices);
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

            // Verwende die vordefinierten Filter in den Filter-Listen
            var allServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            var unusedServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(allServices).Returns(extractedAllServices);
            ServiceInfoExtractorHandler.GetServiceInfo(rootServices).Returns(extractedRootServices);

            // Simuliere das Verhalten des FilterHandlers
            FilterHandler.ApplyFilters(extractedAllServices, allServicesFilters).Returns(extractedAllServices);
            FilterHandler.ApplyFilters(extractedRootServices, rootServicesFilters).Returns(extractedRootServices);

            CoreServiceUsageVerifier.GetUnusedServices(extractedAllServices, extractedRootServices)
                .Returns(unusedServices);

            FilterHandler.ApplyFilters(unusedServices, unusedServicesFilters).Returns(unusedServices);

            // Act
            var result = Verifier.GetUnusedServices(
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

            // Verwende die vordefinierten Filter in den Filter-Listen
            var allServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );

            var rootServicesFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            ServiceInfoExtractorHandler.GetServiceInfo(services).Returns(extractedServices);

            FilterHandler.ApplyFilters(extractedServices, allServicesFilters)
                .Returns(extractedServices);
            FilterHandler.ApplyFilters(extractedServices, rootServicesFilters)
                .Returns(new ServiceInfoSet([ServiceInfo2]));

            CoreServiceUsageVerifier.GetUnusedServices(Arg.Any<ServiceInfoSet>(), Arg.Any<ServiceInfoSet>())
                .Returns([]);

            // Act
            var result = Verifier.GetUnusedServices(
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

    #region Test Interfaces and Classes

    private interface IService1;

    private interface IService2;

    private interface IService3;

    private class Service1 : IService1;

    private class Service2 : IService2;

    private class Service3 : IService3;

    #endregion
}