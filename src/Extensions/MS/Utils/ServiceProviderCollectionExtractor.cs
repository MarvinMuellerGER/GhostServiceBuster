using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Utils;

internal static class ServiceProviderCollectionExtractor
{
    public static IReadOnlyList<ServiceDescriptor> GetAllServiceDescriptorsUnsafe(this IServiceProvider provider) =>
        GetDescriptors(GetCallSiteFactory((ServiceProvider)provider));

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = $"{Constants.PropertyGetter}{Constants.CallSiteFactoryTypeName}")]
    [return: UnsafeAccessorType(Constants.CallSiteFactoryFullNameAndAssembly)]
    private static extern object GetCallSiteFactory(ServiceProvider provider);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = $"{Constants.PropertyGetter}Descriptors")]
    private static extern ServiceDescriptor[] GetDescriptors(
        [UnsafeAccessorType(Constants.CallSiteFactoryFullNameAndAssembly)]
        object callSiteFactory);

    private static class Constants
    {
        private const string DependencyInjectionAssemblyName =
            $"{nameof(Microsoft)}.{nameof(Microsoft.Extensions)}.{nameof(Microsoft.Extensions.DependencyInjection)}";

        private const string DependencyInjectionNamespace = DependencyInjectionAssemblyName;

        private const string ServiceLookupNamespace =
            $"{DependencyInjectionNamespace}.{nameof(Microsoft.Extensions.DependencyInjection.ServiceLookup)}";

        private const string CallSiteFactoryFullName = $"{ServiceLookupNamespace}.{CallSiteFactoryTypeName}";

        internal const string CallSiteFactoryTypeName = "CallSiteFactory";

        internal const string CallSiteFactoryFullNameAndAssembly =
            $"{CallSiteFactoryFullName}, {DependencyInjectionAssemblyName}";

        internal const string PropertyGetter = "get_";
    }
}