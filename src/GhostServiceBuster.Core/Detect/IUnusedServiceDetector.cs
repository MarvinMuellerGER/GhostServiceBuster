using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Detect;

internal interface IUnusedServiceDetector
{
    void RegisterDependencyDetector(DependencyDetector dependencyDetector);

    void RegisterDependencyDetector(IDependencyDetector dependencyDetector) =>
        RegisterDependencyDetector((servicesToAnalyse, potentialDependencies) =>
        {
            var task = dependencyDetector.FindDirectDependencies(servicesToAnalyse, potentialDependencies);
            task.Wait();

            return task.Result;
        });

    void RegisterDependencyDetector(DependencyDetectorTupleResult dependencyDetector) =>
        RegisterDependencyDetector((servicesToAnalyze, potentialDependencies) =>
            new ServiceInfo(dependencyDetector(servicesToAnalyze, potentialDependencies)));

    /// <summary>
    ///     Identifies services that are not used in the dependency chain of the root services.
    /// </summary>
    ServiceInfoSet FindUnusedServices(in ServiceInfoSet allServices, in ServiceInfoSet rootServices);
}