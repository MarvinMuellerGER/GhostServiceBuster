using System.Collections.Generic;

namespace GhostServiceBuster.RegisterMethodsGenerator.Candidates;

internal sealed record RootServiceInfoFilterCandidate(
    string NamespaceName,
    string Name,
    IReadOnlyList<Parameter> Parameters)
    : ServiceInfoFilterCandidate(FilterKind.RootServicesFilter, NamespaceName, Name, Parameters)
{
    public new static string InterfaceName => "GhostServiceBuster.Filter.IRootServiceInfoFilter";
}