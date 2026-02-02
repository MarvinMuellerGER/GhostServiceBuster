using System.Collections.Generic;
using System.Linq;

namespace GhostServiceBuster.RegisterMethodsGenerator.Candidates;

internal abstract record Candidate(string NamespaceName, string Name, IReadOnlyList<Parameter> Parameters)
{
    private string XmlDoc =>
        $"""
                 /// <inheritdoc cref="global::{FullName}"/>
                 /// <remarks>Registers <see cref="global::{FullName}">{Name}</see>.</remarks>
         """;

    private protected string FullName => $"{NamespaceName}.{Name}";

    private protected string ParametersCodeInclTypes =>
        string.Join(", ", Parameters.Select(p => $"global::{p.Type} {p.Name}"));

    private protected string ParametersCodeExclTypes =>
        string.Join(", ", Parameters.Select(p => p.Name));

    public string GetMethodCode(bool forServiceUsageVerifierWithCachedServices) =>
        $"""
         {XmlDoc}
         {GetMethodCodeInternal(forServiceUsageVerifierWithCachedServices)}
         """;

    private protected abstract string GetMethodCodeInternal(bool forServiceUsageVerifierWithCachedServices);
}