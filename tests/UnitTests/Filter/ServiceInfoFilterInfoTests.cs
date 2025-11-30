// ReSharper disable ConvertToLocalFunction

using System.Collections.Immutable;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.UnitTests.Filter;

public static class ServiceInfoFilterInfoTests
{
    public sealed class ImplicitConversion
    {
        private readonly ServiceInfo _serviceInfo1 = new(typeof(IDictionary<,>), typeof(Dictionary<,>));
        private readonly ServiceInfo _serviceInfo2 = new(typeof(IList<>), typeof(List<>));
        private readonly ServiceInfoFilter _serviceInfoFilter = serviceInfo => serviceInfo;
        private readonly SingleServiceInfoFilter _singleServiceInfoFilter = _ => true;

        [Fact]
        public void FromTupleWithServiceInfoFilter_CreatesFilterInfo()
        {
            // Arrange
            var tuple = (Filter: _serviceInfoFilter, IsIndividual: true);

            // Act
            ServiceInfoFilterInfo filterInfo = tuple;

            // Assert
            filterInfo.Should().NotBeNull();
            filterInfo.Filter.Should().Be(_serviceInfoFilter);
            filterInfo.IsIndividual.Should().BeTrue();
        }

        [Fact]
        public void FromTupleWithSingleServiceInfoFilter_CreatesFilterInfo()
        {
            // Arrange
            var tuple = (Filter: _singleServiceInfoFilter, IsIndividual: true);

            // Act
            ServiceInfoFilterInfo filterInfo = tuple;

            // Assert
            filterInfo.Should().NotBeNull();
            filterInfo.IsIndividual.Should().BeTrue();

            // We can't directly compare the filter functions, so we'll test its behavior
            var serviceInfoSet = new ServiceInfoSet(ImmutableHashSet.Create(_serviceInfo1));

            // The converted filter should keep all items for which the original filter returns true
            var result = filterInfo.Filter(serviceInfoSet);
            result.Should().Contain(_serviceInfo1);
        }

        [Fact]
        public void FromServiceInfoFilter_CreatesFilterInfo()
        {
            // Act
            ServiceInfoFilterInfo filterInfo = _serviceInfoFilter;

            // Assert
            filterInfo.Should().NotBeNull();
            filterInfo.Filter.Should().Be(_serviceInfoFilter);
            filterInfo.IsIndividual.Should().BeFalse(); // Default value
        }

        [Fact]
        public void FromSingleServiceInfoFilter_CreatesFilterInfo()
        {
            // Act
            ServiceInfoFilterInfo filterInfo = _singleServiceInfoFilter;

            // Assert
            filterInfo.Should().NotBeNull();
            filterInfo.IsIndividual.Should().BeFalse(); // Default value

            // Test filter behavior
            var serviceInfoSet = new ServiceInfoSet(ImmutableHashSet.Create(_serviceInfo1));

            var result = filterInfo.Filter(serviceInfoSet);
            result.Should().Contain(_serviceInfo1);
        }

        [Fact]
        public void FromSingleServiceInfoFilterInfo_CreatesFilterInfo()
        {
            // Arrange
            var singleFilterInfo = new SingleServiceInfoFilterInfo(_singleServiceInfoFilter, true);

            // Act
            ServiceInfoFilterInfo filterInfo = singleFilterInfo;

            // Assert
            filterInfo.Should().NotBeNull();
            filterInfo.IsIndividual.Should().BeTrue();

            // Test filter behavior
            var serviceInfoSet = new ServiceInfoSet(ImmutableHashSet.Create(_serviceInfo1));

            var result = filterInfo.Filter(serviceInfoSet);
            result.Should().Contain(_serviceInfo1);
        }

        [Fact]
        public void FromSingleServiceInfoFilter_PreservesFilterBehavior()
        {
            // Act
            SingleServiceInfoFilter singleServiceInfoFilter =
                serviceInfo => serviceInfo.ServiceType == _serviceInfo1.ServiceType;
            ServiceInfoFilterInfo filterInfo = singleServiceInfoFilter;

            // Assert
            // Test with matching service
            var serviceInfoSet = new ServiceInfoSet(ImmutableHashSet.Create(_serviceInfo1, _serviceInfo2));

            var result = filterInfo.Filter(serviceInfoSet);
            result.Should().Contain(_serviceInfo1);
            result.Should().NotContain(_serviceInfo2);
        }
    }
}