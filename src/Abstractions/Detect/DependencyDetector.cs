using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

public delegate ServiceInfoSet DependencyDetector(
    ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);

public delegate ServiceInfoTuple DependencyDetectorTupleResult(
    ServiceInfoSet servicesToAnalyse, ServiceInfoSet potentialDependencies);