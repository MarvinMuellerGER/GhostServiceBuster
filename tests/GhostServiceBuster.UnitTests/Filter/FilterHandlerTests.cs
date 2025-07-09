using System.Collections.Immutable;
using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.UnitTests.Filter;

public static class FilterHandlerTests
{
    private static readonly IFilterHandler FilterHandler = new FilterHandler();

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

    private static readonly ServiceInfo ServiceInfo1 = new(typeof(IDictionary<,>), typeof(Dictionary<,>));
    private static readonly ServiceInfo ServiceInfo2 = new(typeof(IList<>), typeof(List<>));
    private static readonly ServiceInfo ServiceInfo3 = new(typeof(IEnumerable<>), typeof(HashSet<>));

    private static readonly ServiceInfoSet ServiceInfoSet = new(ServiceInfo1, ServiceInfo2, ServiceInfo3);

    public sealed class ApplyFilters
    {
        [Fact]
        public void WithNullFilters_ReturnsOriginalSet()
        {
            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, null);

            // Assert
            result.Should().BeEquivalentTo(ServiceInfoSet);
        }

        [Fact]
        public void WithEmptyFiltersList_ReturnsOriginalSet()
        {
            // Arrange
            var emptyFilters = ServiceInfoFilterInfoList.Empty;

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, emptyFilters);

            // Assert
            result.Should().BeEquivalentTo(ServiceInfoSet);
        }

        [Fact]
        public void WithOneNonIndividualFilter_AppliesFilter()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter, true)
            );

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters);

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }

        [Fact]
        public void WithMultipleNonIndividualFilters_AppliesAllFiltersSequentially()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter),
                new ServiceInfoFilterInfo(Service2Filter)
            );

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters);

            // Assert
            // Only Service2 should remain as it's the only one that passes both filters
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo2);
        }

        [Fact]
        public void WithOneIndividualFilter_ReturnsFilterResult()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter, true)
            );

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters);

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }

        [Fact]
        public void WithMultipleIndividualFilters_CombinesResultsWithUnion()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter, true),
                new ServiceInfoFilterInfo(Service2Filter, true)
            );

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters);

            // Assert
            // Both Service1 and Service2 should be included as per the SelectMany implementation
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo2);
        }

        [Fact]
        public void WithMixedIndividualAndNonIndividualFilters_HandlesThemSeparately()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter, true),
                new ServiceInfoFilterInfo(Service2Or3Filter)
            );

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters);

            // Assert
            // Should include Service1 from individual filter and Service2, Service3 from non-individual filter
            result.Should().HaveCount(3);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo2);
            result.Should().Contain(ServiceInfo3);
        }

        [Fact]
        public void WithOneTimeFilter_AppliesItWithOtherFilters()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Or2Filter)
            );
            var oneTimeFilterInfo = new ServiceInfoFilterInfo(Service3Filter);

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters, oneTimeFilterInfo);

            // Assert
            // Since the one-time filter is applied after the regular filter and both are non-individual,
            // the result should be empty (Service3 isn't in the result of the first filter)
            result.Should().BeEmpty();
        }

        [Fact]
        public void WithIndividualOneTimeFilter_IncludesItsResultsWithOtherFilters()
        {
            // Arrange
            var filters = new ServiceInfoFilterInfoList
            (
                new ServiceInfoFilterInfo(Service1Filter)
            );
            var oneTimeFilterInfo = new ServiceInfoFilterInfo(Service3Filter, true);

            // Act
            var result = FilterHandler.ApplyFilters(ServiceInfoSet, filters, oneTimeFilterInfo);

            // Assert
            // Should include Service1 from non-individual filter and Service3 from individual one-time filter
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo3);
        }
    }
}