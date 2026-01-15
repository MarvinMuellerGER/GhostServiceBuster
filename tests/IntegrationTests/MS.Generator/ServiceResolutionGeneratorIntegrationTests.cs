using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GhostServiceBuster.IntegrationTests.Testees;
using GhostServiceBuster.MS.Generator;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace  GhostServiceBuster.IntegrationTests.MS.Generator;

public sealed class ServiceResolutionGeneratorIntegrationTests
{
    [Fact]
    private void TestMethod()
    {
        var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<TypesResolvedByServiceProviderAttribute>()!;
        var typesResolvedByServiceProvider = attribute.Types;

        typesResolvedByServiceProvider.Should().BeEquivalentTo([
            typeof(IServiceResolvedByServiceProvider),
            typeof(IServiceResolvedByServiceProvider1),
            typeof(IServiceResolvedByServiceProvider2),
            typeof(IServiceResolvedByServiceProvider3),
            typeof(IServiceResolvedByServiceProvider4),
            typeof(IServiceResolvedByServiceProvider5),
            typeof(IServiceResolvedByServiceProvider6),
            typeof(IServiceResolvedByServiceProvider7),
            typeof(IServiceResolvedByServiceProvider8),
            typeof(IServiceResolvedByServiceProvider9),
            typeof(IServiceResolvedByServiceProvider10),
            typeof(IServiceResolvedByServiceProvider11),
            typeof(IServiceResolvedByServiceProvider12)
        ]);
    }

    [UsedImplicitly]
    [SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
    private static void ServiceProviderInvocations()
    {
        IKeyedServiceProvider serviceProvider = null!;
        const string serviceKey = "ServiceKey";

        _ = serviceProvider.GetServices(typeof(IServiceResolvedByServiceProvider1));
        _ = serviceProvider.GetService(typeof(IServiceResolvedByServiceProvider2));
        _ = serviceProvider.GetRequiredService(typeof(IServiceResolvedByServiceProvider3));
        _ = serviceProvider.GetRequiredKeyedService(typeof(IServiceResolvedByServiceProvider4), serviceKey);
        _ = serviceProvider.GetKeyedService(typeof(IServiceResolvedByServiceProvider5), serviceKey);
        _ = serviceProvider.GetKeyedServices(typeof(IServiceResolvedByServiceProvider6), serviceKey);

        _ = serviceProvider.GetServices<IServiceResolvedByServiceProvider7>();
        _ = serviceProvider.GetService<IServiceResolvedByServiceProvider8>();
        _ = serviceProvider.GetRequiredService<IServiceResolvedByServiceProvider9>();
        _ = serviceProvider.GetRequiredKeyedService<IServiceResolvedByServiceProvider10>(serviceKey);
        _ = serviceProvider.GetKeyedService<IServiceResolvedByServiceProvider11>(serviceKey);
        _ = serviceProvider.GetKeyedServices<IServiceResolvedByServiceProvider12>(serviceKey);
    }
}