using System.Collections.Generic;

namespace GhostServiceBuster.RegisterMethodsGenerator.Candidates;

internal record ServiceInfoFilterCandidate(
    FilterKind FilterKind,
    string NamespaceName,
    string Name,
    IReadOnlyList<Parameter> Parameters)
    : Candidate(NamespaceName, Name, Parameters)
{
    public static string InterfaceName => "GhostServiceBuster.Filter.IServiceInfoFilter";

    private string NameWithoutSuffix => Name.Replace("Filter", string.Empty);

    private protected override string GetMethodCodeInternal(bool forServiceUsageVerifierWithCachedServices) =>
        $"""
                 public {GetMethodReturnType(forServiceUsageVerifierWithCachedServices)} Register{NameWithoutSuffix}{FilterKind}({ParametersCodeInclTypes}) =>
                     serviceUsageVerifier.Register{FilterKind}{(Parameters.Count is 0
                             ? $"""
                                <
                                                 global::{FullName}>();
                                """
                             : $"""
                                (
                                                 new global::{FullName}({ParametersCodeExclTypes}));
                                """
                         )}
         """;

    private static string GetMethodReturnType(bool forServiceUsageVerifierWithCachedServices) =>
        forServiceUsageVerifierWithCachedServices
            ? "IServiceUsageVerifierWithCachedServicesAndFiltersMutable"
            : "IServiceUsageVerifierWithCachedFiltersMutable";
}