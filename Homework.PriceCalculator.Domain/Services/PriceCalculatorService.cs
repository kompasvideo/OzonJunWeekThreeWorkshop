using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Models;
using Homework.PriceCalculator.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Homework.PriceCalculator.Domain.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private readonly double _volumeToPriceRatio;
    private readonly double _weightToPriceRatio;
    private readonly IStorageRepository _storageRepository;

    public PriceCalculatorService( 
        // double volumeToPriceRatio, double weightToPriceRatio,
        PriceCalculatorOptions options,
        IStorageRepository storageRepository)
    {
        // _volumeToPriceRatio = volumeToPriceRatio;
        // _weightToPriceRatio = weightToPriceRatio;
        _volumeToPriceRatio = options.VolumeToPriceRatio;
        _weightToPriceRatio = options.WeightToPriceRatio;
        _storageRepository = storageRepository;
    }

    public double CalculatePrice(GoodModel[] goods)
    {
        if (goods.Any())
        {
            throw new ArgumentException("Список товаров пустой");
        }

        var volumePrice = CalculatePriceByVolume(goods, out var volume);
        var weightPrice = CalculatePriceByWeight(goods, out var weight);
        var resultPrice = Math.Max(volumePrice, weightPrice);
        _storageRepository.Save(new StorageEntity(volume, resultPrice, DateTime.UtcNow, weight));
        return resultPrice;
    }

    private double CalculatePriceByVolume(GoodModel[] goods, out int volume)
    {
        volume = goods.Sum(x => x.Length * x.Height * x.Width);
        return _volumeToPriceRatio * volume / 1000.0d;
    }

    private double CalculatePriceByWeight(GoodModel[] goods, out double weight)
    {
        weight = goods.Sum(x => x.Weight);
        return _weightToPriceRatio * weight;
    }
    
    public CalculationLogModel[] QueryLog(int take)
    {
        if (take <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "Take должно быть больше нуля");
        }
        var log = _storageRepository.Query()
            .OrderByDescending(x => x.At)
            .Take(take)
            .ToArray();
        return log
            .Select(x => new CalculationLogModel(
                x.Volume, 
                x.Price,
                x.Weight))
            .ToArray();
    }
}