using Microsoft.AspNetCore.Mvc;

namespace GhostServiceBuster.IntegrationTests.Testees;

[ApiController]
[Route("test")]
public sealed class TestController(IServiceInjectedIntoController service) : ControllerBase
{
    [HttpGet]
    public string Test() => service.GetType().Name;
}