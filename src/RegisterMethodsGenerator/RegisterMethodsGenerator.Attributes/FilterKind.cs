namespace GhostServiceBuster.RegisterMethodsGenerator;

/// <summary>
///     Defines to which kind of services a filter is applied.
/// </summary>
public enum FilterKind
{
    /// <summary>
    ///     Defines that a filter is applied to all services.
    /// </summary>
    AllServicesFilter,

    /// <summary>
    ///     Defines that a filter is applied to root services.
    /// </summary>
    RootServicesFilter,

    /// <summary>
    ///     Defines that a filter is applied to unused services.
    /// </summary>
    UnusedServicesFilter
}