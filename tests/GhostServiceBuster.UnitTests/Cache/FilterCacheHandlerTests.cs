using System.Collections.Immutable;
using GhostServiceBuster.Cache;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;
using NSubstitute;

namespace GhostServiceBuster.UnitTests.Cache;

public static class FilterCacheHandlerTests
{
    private static readonly ServiceInfo ServiceInfo1 = new(typeof(IService1), typeof(Service1));
    private static readonly ServiceInfo ServiceInfo2 = new(typeof(IService2), typeof(Service2));

    private static readonly ServiceInfoSet ServiceInfoSet = new(ServiceInfo1, ServiceInfo2);

    private static readonly ServiceInfoFilter Service1Filter = services =>
        services.Where(s => s == ServiceInfo1).ToImmutableHashSet();

    private static readonly ServiceInfoFilter Service2Filter = services =>
        services.Where(s => s == ServiceInfo2).ToImmutableHashSet();

    public sealed class RegisterFilters
    {
        [Fact]
        public void CumulatesFiltersForSubsequentCalls()
        {
            // Arrange
            var filterHandler = Substitute.For<IFilterHandler>();
            var filterCacheHandler = new FilterCacheHandler(filterHandler);


            var filters1 = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Filter)
            );

            var filters2 = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            // Act
            filterCacheHandler.RegisterFilters(filters1);
            filterCacheHandler.RegisterFilters(filters2);

            filterCacheHandler.ApplyFilters(ServiceInfoSet, ServiceInfoFilterInfoList.Empty);

            // Assert
            filterHandler.Received(1).ApplyFilters(
                ServiceInfoSet,
                Arg.Is<ServiceInfoFilterInfoList>(list =>
                    list.Count == 2 &&
                    list[0].Filter == Service1Filter &&
                    list[1].Filter == Service2Filter));
        }
    }

    public sealed class ApplyFilters
    {
        [Fact]
        public void DelegatesCallToFilterHandlerWithCorrectParameters()
        {
            // Arrange
            var filterHandler = Substitute.For<IFilterHandler>();
            var filterCacheHandler = new FilterCacheHandler(filterHandler);


            var oneTimeFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            // Act
            filterCacheHandler.ApplyFilters(ServiceInfoSet, oneTimeFilters);

            // Assert
            filterHandler.Received(1).ApplyFilters(
                ServiceInfoSet,
                Arg.Is<ServiceInfoFilterInfoList>(list =>
                    list.Count == 1 &&
                    list[0].Filter == Service2Filter));
        }

        [Fact]
        public void WithRegisteredFilters_DelegatesCallWithCombinedFilters()
        {
            // Arrange
            var filterHandler = Substitute.For<IFilterHandler>();
            var filterCacheHandler = new FilterCacheHandler(filterHandler);


            var registeredFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service1Filter)
            );

            var oneTimeFilters = new ServiceInfoFilterInfoList(
                new ServiceInfoFilterInfo(Service2Filter)
            );

            filterCacheHandler.RegisterFilters(registeredFilters);

            // Act
            filterCacheHandler.ApplyFilters(ServiceInfoSet, oneTimeFilters);

            // Assert
            filterHandler.Received(1).ApplyFilters(
                ServiceInfoSet,
                Arg.Is<ServiceInfoFilterInfoList>(list =>
                    list.Count == 2 &&
                    list[0].Filter == Service1Filter &&
                    list[1].Filter == Service2Filter));
        }
    }

    #region Test Interfaces and Classes

    private interface IService1;

    private interface IService2;

    private class Service1 : IService1;

    private class Service2 : IService2;

    #endregion
}