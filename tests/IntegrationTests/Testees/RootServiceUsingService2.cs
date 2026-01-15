#pragma warning disable CS9113 // Parameter is unread.

namespace GhostServiceBuster.IntegrationTests.Testees;

internal class RootServiceUsingService2(IService2 service1) : IRootServiceUsingService2;