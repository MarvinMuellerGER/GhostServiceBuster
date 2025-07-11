using System.Collections.Immutable;
using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.UnitTests.Collections;

public static class ServiceInfoSetTests
{
    private static readonly ServiceInfo Service1 = new(typeof(IDictionary<,>), typeof(Dictionary<,>));
    private static readonly ServiceInfo Service2 = new(typeof(IList<>), typeof(List<>));

    public sealed class ImplicitConversion
    {
        [Fact]
        public void FromEmptyImmutableHashSet_CreatesEmptyServiceInfoSet()
        {
            // Arrange
            var immutableSet = ImmutableHashSet<ServiceInfo>.Empty;

            // Act
            ServiceInfoSet serviceInfoSet = immutableSet;

            // Assert
            serviceInfoSet.Should().NotBeNull();
            serviceInfoSet.Should().BeEmpty();
        }

        [Fact]
        public void FromImmutableHashSet_CreatesServiceInfoSetWithSameItems()
        {
            // Arrange
            var immutableSet = ImmutableHashSet.Create(Service1, Service2);

            // Act
            ServiceInfoSet serviceInfoSet = immutableSet;

            // Assert
            serviceInfoSet.Should().NotBeNull();
            serviceInfoSet.Should().HaveCount(2);
            serviceInfoSet.Should().Contain(Service1);
            serviceInfoSet.Should().Contain(Service2);
        }

        [Fact]
        public void PreservesSetSemantics()
        {
            // Arrange
            var immutableSet = ImmutableHashSet.Create(Service1, Service1, Service2);

            // Act
            ServiceInfoSet serviceInfoSet = immutableSet;

            // Assert
            serviceInfoSet.Should().HaveCount(2);
        }
    }
}