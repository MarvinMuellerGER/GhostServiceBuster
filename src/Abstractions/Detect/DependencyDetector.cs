using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

/// <summary>
/// Represents a dependency detector that returns a full <see cref="ServiceInfoSet"/>.
/// </summary>
/// <param name="servicesToAnalyse">The services whose dependencies should be evaluated.</param>
/// <param name="potentialDependencies">The services that can be considered dependencies.</param>
public delegate ServiceInfoSet DependencyDetector(
    ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);

/// <summary>
/// Represents a dependency detector that returns a tuple projection.
/// </summary>
/// <param name="servicesToAnalyse">The services whose dependencies should be evaluated.</param>
/// <param name="potentialDependencies">The services that can be considered dependencies.</param>
public delegate ServiceInfoTuple DependencyDetectorTupleResult(
    ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);
