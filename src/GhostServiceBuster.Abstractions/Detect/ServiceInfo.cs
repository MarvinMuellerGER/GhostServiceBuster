namespace GhostServiceBuster.Detect;

public record ServiceInfo(Type ServiceType, Type ImplementationType)
{
    public ServiceInfo(ServiceInfoTuple tuple) : this(tuple.ServiceType, tuple.ImplementationType)
    {
    }
}