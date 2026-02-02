// ReSharper disable InconsistentNaming

#pragma warning disable CS9113 // Parameter is unread.

namespace GhostServiceBuster.RegisterMethodsGenerator;

/// <inheritdoc cref="GenerateRegisterMethodForAttribute" />
/// <param name="As">Defines as which <see cref="FilterKind">kind</see> the filter should be registered.</param>
/// <remarks>
///     Is only valid on types that implement either
///     <see cref="GhostServiceBuster.Filter.IServiceInfoFilter">IServiceInfoFilter</see> or
///     <see cref="GhostServiceBuster.Filter.IRootServiceInfoFilter">IRootServiceInfoFilter</see>.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GenerateRegisterMethodForFilterAttribute(FilterKind As) : Attribute;