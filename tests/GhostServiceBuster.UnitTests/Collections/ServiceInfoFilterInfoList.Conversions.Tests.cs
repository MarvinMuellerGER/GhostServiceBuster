using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.UnitTests.Collections;

public static class ServiceInfoFilterInfoListTests
{
    private static readonly ServiceInfoFilter ServiceInfoFilter = serviceInfo => serviceInfo;
    private static readonly SingleServiceInfoFilter SingleServiceInfoFilter = _ => true;

    public sealed class ImplicitConversion
    {
        [Fact]
        public void FromNullTupleWithServiceInfoFilter_CreatesEmptyList()
        {
            // Arrange
            (ServiceInfoFilter Filter, bool IsIndividual)? tuple = null;

            // Act
            ServiceInfoFilterInfoList list = tuple;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromTupleWithServiceInfoFilter_CreatesListWithOneItem()
        {
            // Arrange
            (ServiceInfoFilter Filter, bool IsIndividual) tuple = (ServiceInfoFilter, true);

            // Act
            ServiceInfoFilterInfoList list = tuple;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Filter.Should().Be(ServiceInfoFilter);
            list[0].IsIndividual.Should().BeTrue();
        }

        [Fact]
        public void FromNullTupleWithSingleServiceInfoFilter_CreatesEmptyList()
        {
            // Arrange
            (SingleServiceInfoFilter Filter, bool IsIndividual)? tuple = null;

            // Act
            ServiceInfoFilterInfoList list = tuple;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromTupleWithSingleServiceInfoFilter_CreatesListWithOneItem()
        {
            // Arrange
            (SingleServiceInfoFilter Filter, bool IsIndividual) tuple = (SingleServiceInfoFilter, false);

            // Act
            ServiceInfoFilterInfoList list = tuple;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Filter.Should().NotBeNull();
            list[0].IsIndividual.Should().BeFalse();
        }

        [Fact]
        public void FromNullServiceInfoFilter_CreatesEmptyList()
        {
            // Arrange
            ServiceInfoFilter? filter = null;

            // Act
            ServiceInfoFilterInfoList list = filter;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromServiceInfoFilter_CreatesListWithOneItem()
        {
            // Act
            ServiceInfoFilterInfoList list = ServiceInfoFilter;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Filter.Should().Be(ServiceInfoFilter);
            list[0].IsIndividual.Should().BeFalse(); // Default value for IsIndividual
        }

        [Fact]
        public void FromNullSingleServiceInfoFilter_CreatesEmptyList()
        {
            // Arrange
            SingleServiceInfoFilter? filter = null;

            // Act
            ServiceInfoFilterInfoList list = filter;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromSingleServiceInfoFilter_CreatesListWithOneItem()
        {
            // Act
            ServiceInfoFilterInfoList list = SingleServiceInfoFilter;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Filter.Should().NotBeNull();
            list[0].IsIndividual.Should().BeFalse(); // Default value for IsIndividual
        }

        [Fact]
        public void FromNullSingleServiceInfoFilterInfo_CreatesEmptyList()
        {
            // Arrange
            SingleServiceInfoFilterInfo? filterInfo = null;

            // Act
            ServiceInfoFilterInfoList list = filterInfo;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromSingleServiceInfoFilterInfo_CreatesListWithOneItem()
        {
            // Arrange
            var filterInfo = new SingleServiceInfoFilterInfo(SingleServiceInfoFilter, true);

            // Act
            ServiceInfoFilterInfoList list = filterInfo;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Filter.Should().NotBeNull();
            list[0].IsIndividual.Should().Be(filterInfo.IsIndividual);
        }

        [Fact]
        public void FromNullServiceInfoFilterInfo_CreatesEmptyList()
        {
            // Arrange
            ServiceInfoFilterInfo? filterInfo = null;

            // Act
            ServiceInfoFilterInfoList list = filterInfo;

            // Assert
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public void FromServiceInfoFilterInfo_CreatesListWithOneItem()
        {
            // Arrange
            var filterInfo = new ServiceInfoFilterInfo(ServiceInfoFilter, true);

            // Act
            ServiceInfoFilterInfoList list = filterInfo;

            // Assert
            list.Should().NotBeNull();
            list.Should().HaveCount(1);
            list[0].Should().Be(filterInfo);
        }
    }
}