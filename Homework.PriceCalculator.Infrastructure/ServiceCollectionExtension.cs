using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services.Interfaces;
using Homework.PriceCalculator.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Homework.PriceCalculator.Domain.Services;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PriceCalculatorOptions>(configuration.GetSection("PriceCalculatorOptions"));
        // services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(
        //     x => new PriceCalculatorService(
        //         x.GetRequiredService<IOptions<PriceCalculatorOptions>>(),
        //         // _configuration.GetValue<double>("PriceCalculatorOptions:VolumeToPriceRatio"),
        //         // _configuration.GetValue<double>("PriceCalculatorOptions:WeightToPriceRatio"),
        //         x.GetRequiredService<IStorageRepository>()
        //         ));
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(x =>
        {
            var options = x.GetRequiredService<IOptionsSnapshot<PriceCalculatorOptions>>().Value;
            return new PriceCalculatorService(options, x.GetRequiredService<IStorageRepository>());
        });
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddSingleton<IGoodsRepository, GoodsRepository>();
        services.AddScoped<IGoodsService, GoodsService>();
        return services;
    }
}