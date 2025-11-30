using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;

namespace GhostServiceBuster.Default.Extract;

file sealed class TypeServiceInfoExtractor : IServiceInfoExtractor<Type>
{
    public ServiceInfoSet ExtractServiceInfos(Type type) =>
        (type.IsInterface ? type : type.GetInterfaces().FirstOrDefault() ?? type, type);
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterTypeServiceInfoExtractor(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterServiceInfoExtractor<TypeServiceInfoExtractor, Type>();
}