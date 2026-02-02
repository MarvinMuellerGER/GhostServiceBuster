using GhostServiceBuster.Collections;
using GhostServiceBuster.Extract;
using GhostServiceBuster.RegisterMethodsGenerator;

namespace GhostServiceBuster.Default.Extract;

[GenerateRegisterMethodFor]
internal sealed class TypeServiceInfoExtractor : IServiceInfoExtractor<Type>
{
    public ServiceInfoSet ExtractServiceInfos(Type type) =>
        (type.IsInterface ? type : type.GetInterfaces().FirstOrDefault() ?? type, type);
}