#pragma warning disable CS9113 // Parameter is unread.

namespace GhostServiceBuster.IntegrationTests.Testees;

internal interface IRootServiceUsingService2;

internal sealed class RootServiceUsingService2(IService2 service2) : IRootServiceUsingService2;