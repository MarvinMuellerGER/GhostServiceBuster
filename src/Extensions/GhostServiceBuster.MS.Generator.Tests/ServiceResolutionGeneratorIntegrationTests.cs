using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Generator.Tests;

public sealed class ServiceResolutionGeneratorIntegrationTests
{
    [Fact]
    private void TestMethod()
    {
        var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<TypesResolvedByServiceProviderAttribute>()!;
        var typesResolvedByServiceProvider = attribute.Types;

        typesResolvedByServiceProvider.Should().BeEquivalentTo([
            typeof(IService1),
            typeof(IService2),
            typeof(IService3),
            typeof(IService4),
            typeof(IService5),
            typeof(IService6),
            typeof(IService7),
            typeof(IService8),
            typeof(IService9),
            typeof(IService10),
            typeof(IService11)
        ]);
    }

    [UsedImplicitly]
    [SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
    private static void ServiceProviderInvocations()
    {
        IKeyedServiceProvider serviceProvider = null!;
        const string serviceKey = "ServiceKey";

        _ = serviceProvider.GetServices(typeof(IService1));
        _ = serviceProvider.GetService(typeof(IService2));
        _ = serviceProvider.GetRequiredService(typeof(IService3));
        _ = serviceProvider.GetRequiredKeyedService(typeof(IService4), serviceKey);
        _ = serviceProvider.GetKeyedService(typeof(IService5), serviceKey);
        _ = serviceProvider.GetKeyedServices(typeof(IService6), serviceKey);

        _ = serviceProvider.GetServices<IService7>();
        _ = serviceProvider.GetService<IService8>();
        _ = serviceProvider.GetRequiredService<IService9>();
        _ = serviceProvider.GetRequiredKeyedService<IService9>(serviceKey);
        _ = serviceProvider.GetKeyedService<IService10>(serviceKey);
        _ = serviceProvider.GetKeyedServices<IService11>(serviceKey);
    }

    internal interface IService1;

    internal interface IService2;

    internal interface IService3;

    internal interface IService4;

    internal interface IService5;

    internal interface IService6;

    internal interface IService7;

    internal interface IService8;

    internal interface IService9;

    internal interface IService10;

    internal interface IService11;
}