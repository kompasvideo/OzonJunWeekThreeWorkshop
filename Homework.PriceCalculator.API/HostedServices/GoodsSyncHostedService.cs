using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services.Interfaces;

namespace Workshop.Api.HostedServices;

public class GoodsSyncHostedService : IHostedService
{
    private readonly IGoodsRepository _goodsRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly IGoodsService _service;

    public GoodsSyncHostedService(IGoodsRepository goodsRepository, 
        //IGoodsService service)
        IServiceProvider serviceProvider)
    {
        _goodsRepository = goodsRepository;
        _serviceProvider = serviceProvider;
        // _service = service;
    }
    private async Task ExecuteAdsync()
    {
        while (true)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IGoodsService>();
                var goods = _service.GetGoods().ToList();
                foreach (var good in goods)
                {
                    _goodsRepository.AddOrUpdate(good);
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(10));
        }    
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ExecuteAdsync();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}