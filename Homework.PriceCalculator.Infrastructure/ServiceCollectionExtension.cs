using Homework.PriceCalculator.Domain.Separated;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Infrastructure.Dal.Repositories;
using Homework.PriceCalculator.Infrastructure.External;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.PriceCalculator.Domain.Services;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddSingleton<IGoodsRepository, GoodsRepository>();
        services.AddScoped<IGoodsService, GoodsService>();
        return services;
    }
}