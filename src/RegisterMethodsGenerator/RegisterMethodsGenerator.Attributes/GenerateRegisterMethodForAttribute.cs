namespace GhostServiceBuster.RegisterMethodsGenerator;

/// <summary>
///     Defines that a method for registering in a
///     <see cref="GhostServiceBuster.IServiceUsageVerifier">ServiceUsageVerifier</see>
///     should be generated for the candidate.
/// </summary>
/// <remarks>
///     Is only valid on types that implement either
///     <see cref="GhostServiceBuster.Detect.IDependencyDetector">IDependencyDetector</see> or
///     <see cref="GhostServiceBuster.Extract.IServiceInfoExtractor{TServiceCollection}">IServiceInfoExtractor</see> or
///     <see cref="GhostServiceBuster.Filter.IServiceInfoFilter">IServiceInfoFilter</see> or
///     <see cref="GhostServiceBuster.Filter.IRootServiceInfoFilter">IRootServiceInfoFilter</see>
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GenerateRegisterMethodForAttribute : Attribute;