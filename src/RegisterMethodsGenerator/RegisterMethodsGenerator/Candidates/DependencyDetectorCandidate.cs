using System.Collections.Generic;

namespace GhostServiceBuster.RegisterMethodsGenerator.Candidates;

internal sealed record DependencyDetectorCandidate(
    string NamespaceName,
    string Name,
    IReadOnlyList<Parameter> Parameters) : Candidate(NamespaceName, Name, Parameters)
{
    public static string InterfaceName => "GhostServiceBuster.Detect.IDependencyDetector";

    private protected override string GetMethodCodeInternal(bool _) =>
        $"""
                  public TServiceUsageVerifier Register{Name}({ParametersCodeInclTypes}) =>
                      (TServiceUsageVerifier)serviceUsageVerifier.RegisterDependencyDetector{(Parameters.Count is 0
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
}