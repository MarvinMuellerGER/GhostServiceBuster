using Microsoft.AspNetCore.Mvc;

namespace GhostServiceBuster.IntegrationTests.Testees;

[ApiController]
[Route("test")]
public sealed class TestController(IService2 service) : ControllerBase
{
    [HttpGet]
    public string Test() => service.GetType().Name;
}