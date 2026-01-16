#pragma warning disable CS9113 // Parameter is unread.

namespace GhostServiceBuster.IntegrationTests.Testees;

internal sealed class MessageHandlerUsingService2(IService2 service2) : IHandleMessages<object>
{
    public Task Handle(object message, IMessageHandlerContext context) => throw new InvalidOperationException();
}