using System.Linq;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Todo.Api;
using Todo.Infrastructure.DatabaseContext;
using Todo.Tests.Fixture.EF;

namespace Todo.Tests.Fixture
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseEnvironment("Test")
                .ConfigureWebHost(webHostBuilder => webHostBuilder.UseStartup<Startup>().UseTestServer());

        protected override void ConfigureWebHost(IWebHostBuilder builder) => builder.ConfigureTestServices(
            services =>
            {
                foreach (var hostedService in services.Where(service => service is IHostedService).ToList())
                {
                    services.Remove(hostedService);
                }
            });
    }
}
