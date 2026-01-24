using GhostServiceBuster.AspNet;
using GhostServiceBuster.IntegrationTests.Testees;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.IntegrationTests.AspNet;

public static class AspNetIntegrationTests
{
    public sealed class FindUnusedServices
    {
        [Fact]
        public async Task WithRealDependencies_ReturnsCorrectResult()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(TestController).Assembly);

            builder.Services.AddTransient<IService1, Service1>();
            builder.Services.AddTransient<IService2, Service2>();
            builder.Services.AddSingleton(builder.Services);

            var app = builder.Build();
            app.MapControllers();
            await app.StartAsync();

            var serviceUsageVerifier = Verify.New.ForAspNet(app.Services);

            // Act
            var unusedServices = serviceUsageVerifier.FindUnusedServices();

            // Assert
            unusedServices.Should().HaveCount(1);
            unusedServices.Should().Contain(s => s.ServiceType == typeof(IService1));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(IService2));
            unusedServices.Should().NotContain(s => s.ServiceType == typeof(TestController));
        }
    }
}

[ApiController]
[Route("test")]
public sealed class TestController(IService2 service) : ControllerBase
{
    [HttpGet]
    public string Test() => service.GetType().Name;
}