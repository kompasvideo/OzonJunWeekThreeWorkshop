using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.PriceCalculator.Domain.IntegrationTests;

public class AppFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddControllers().AddControllersAsServices();
        });
        
        base.ConfigureWebHost(builder);
    }
}