// ReSharper disable ConvertToLocalFunction

using System.Collections.Immutable;
using FluentAssertions;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;

namespace GhostServiceBuster.UnitTests.Extract;

public static class ServiceInfoExtractorHandlerTests
{
    private static readonly ServiceInfo ServiceInfo1 = new(typeof(IDictionary<,>), typeof(Dictionary<,>));
    private static readonly ServiceInfo ServiceInfo2 = new(typeof(IList<>), typeof(List<>));

    public sealed class RegisterServiceInfoExtractor
    {
        [Fact]
        public void ForNewType_AddsExtractor()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            var testList = new List<string> { "Service1" };
            ServiceInfoExtractor<List<string>> extractor = _ => new ServiceInfoSet([ServiceInfo1]);

            // Act
            handler.RegisterServiceInfoExtractor(extractor);

            // Assert
            var result = handler.GetServiceInfo(testList);
            result.Should().HaveCount(1);
            result.Should().Contain(ServiceInfo1);
        }

        [Fact]
        public void ForAlreadyRegisteredType_ThrowsException()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            ServiceInfoExtractor<List<string>> extractor1 = _ => new ServiceInfoSet([ServiceInfo1]);
            ServiceInfoExtractor<List<string>> extractor2 = _ => new ServiceInfoSet([ServiceInfo2]);

            // Register first extractor
            handler.RegisterServiceInfoExtractor(extractor1);

            // Act & Assert
            var act = () => handler.RegisterServiceInfoExtractor(extractor2);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*A service info extractor for System.Collections.Generic.List`1*is already registered*");
        }
    }

    public sealed class GetServiceInfo
    {
        [Fact]
        public void WithRegisteredExtractor_ReturnsExtractedInfo()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            var testDictionary = new Dictionary<string, string>
            {
                { "Service1", "1.0" },
                { "Service2", "2.0" }
            };

            ServiceInfoExtractor<Dictionary<string, string>> extractor =
                _ => new ServiceInfoSet([ServiceInfo1, ServiceInfo2]);

            handler.RegisterServiceInfoExtractor(extractor);

            // Act
            var result = handler.GetServiceInfo(testDictionary);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(ServiceInfo1);
            result.Should().Contain(ServiceInfo2);
        }

        [Fact]
        public void WithServiceInfoSetInput_ReturnsSameInstance()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            var serviceInfoSet = new ServiceInfoSet([ServiceInfo1]);

            // Act
            var result = handler.GetServiceInfo(serviceInfoSet);

            // Assert
            result.Should().BeSameAs(serviceInfoSet);
        }

        [Fact]
        public void WithNoRegisteredExtractor_ThrowsException()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            var testList = new List<string> { "Service1" };

            // Act & Assert
            Action act = () => handler.GetServiceInfo(testList);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*No service info extractor registered for System.Collections.Generic.List`1*");
        }

        [Fact]
        public void WithMultipleRegisteredExtractors_UsesCorrectExtractor()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();

            // Register extractor for List<string>
            ServiceInfoExtractor<List<string>> listExtractor = _ => new ServiceInfoSet([ServiceInfo1]);
            handler.RegisterServiceInfoExtractor(listExtractor);

            // Register extractor for Dictionary<string, string>
            ServiceInfoExtractor<Dictionary<string, string>> dictExtractor = _ => new ServiceInfoSet([ServiceInfo2]);
            handler.RegisterServiceInfoExtractor(dictExtractor);

            var testList = new List<string> { "ServiceFromList" };
            var testDict = new Dictionary<string, string> { { "ServiceFromDict", "2.0" } };

            // Act
            var listResult = handler.GetServiceInfo(testList);
            var dictResult = handler.GetServiceInfo(testDict);

            // Assert
            listResult.Should().HaveCount(1);
            listResult.Should().Contain(ServiceInfo1);

            dictResult.Should().HaveCount(1);
            dictResult.Should().Contain(ServiceInfo2);
        }

        [Fact]
        public void WithDerivedType_WorksWithBaseType()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();

            // Register extractor for object (base class)
            ServiceInfoExtractor<object> objectExtractor = _ => new ServiceInfoSet([ServiceInfo1]);
            handler.RegisterServiceInfoExtractor(objectExtractor);

            // Try to get service info for a string (derived from object)
            var testString = "TestService";

            // Act & Assert
            // This should throw because we need exact type match, not inheritance-based matching
            Action act = () => handler.GetServiceInfo(testString);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*No service info extractor registered for System.String*");
        }

        [Fact]
        public void RegisterAndWithCustomType_WorksCorrectly()
        {
            // Arrange
            var handler = new ServiceInfoExtractorHandler();
            var customCollection = new CustomServiceCollection
            {
                Services = [typeof(IList<>), typeof(IDictionary<,>), typeof(IEnumerable<>)]
            };

            ServiceInfoExtractor<CustomServiceCollection> extractor = collection =>
                new ServiceInfoSet(
                    collection.Services.Select((type, index) =>
                        new ServiceInfo(type, index switch
                        {
                            0 => typeof(List<>),
                            1 => typeof(Dictionary<,>),
                            _ => typeof(HashSet<>)
                        })).ToImmutableHashSet()
                );

            // Act
            handler.RegisterServiceInfoExtractor(extractor);
            var result = handler.GetServiceInfo(customCollection);

            // Assert
            result.Should().HaveCount(3);
            result.Should().Contain(new ServiceInfo(typeof(IList<>), typeof(List<>)));
            result.Should().Contain(new ServiceInfo(typeof(IDictionary<,>), typeof(Dictionary<,>)));
            result.Should().Contain(new ServiceInfo(typeof(IEnumerable<>), typeof(HashSet<>)));
        }
    }

    // Helper class for testing
    private class CustomServiceCollection
    {
        public List<Type> Services { get; set; } = [];
    }
}