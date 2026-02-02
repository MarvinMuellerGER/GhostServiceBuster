// ReSharper disable InconsistentNaming
#pragma warning disable CS9113 // Parameter is unread.

namespace GhostServiceBuster.RegisterMethodsGenerator;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GenerateRegisterMethodForFilterAttribute(FilterKind As) : Attribute;