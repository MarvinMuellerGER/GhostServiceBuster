using System.Collections.Generic;

namespace GhostServiceBuster.RegisterMethodsGenerator.Candidates;

internal sealed record ServiceInfoExtractorCandidate(
    string ServiceCollectionType,
    string NamespaceName,
    string Name,
    IReadOnlyList<Parameter> Parameters)
    : Candidate(NamespaceName, Name, Parameters)
{
    public static string InterfaceName => "GhostServiceBuster.Extract.IServiceInfoExtractor";

    private protected override string GetMethodCodeInternal(bool _) =>
        $"""
                 public TServiceUsageVerifier Register{Name}({ParametersCodeInclTypes}) =>
                     (TServiceUsageVerifier)serviceUsageVerifier.RegisterServiceInfoExtractor{(Parameters.Count is 0
                             ? $"""
                                <
                                                 global::{FullName}, global::{ServiceCollectionType}>();
                                """
                             : $"""
                                (
                                                 new global::{FullName}({ParametersCodeExclTypes}));
                                """
                         )}
         """;
}