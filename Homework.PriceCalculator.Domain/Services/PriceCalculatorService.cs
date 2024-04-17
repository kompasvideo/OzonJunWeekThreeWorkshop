using FluentValidation;
using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Exceptions;
using Homework.PriceCalculator.Domain.Models;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services.Interfaces;
using Homework.PriceCalculator.Domain.Validators;

namespace Homework.PriceCalculator.Domain.Services;

internal sealed class PriceCalculatorService : IPriceCalculatorService
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

    public double CalculatePrice(IReadOnlyList<GoodModel> goods)
    {
        try
        {
            return CalculateUnsave(goods);
        }
        catch (ValidationException e)
        {
            throw new DomainException("incorrect input", e);
        }
        catch (DivideByZeroException e)
        {
            throw new DomainException("division by zero", e);
        }
    }

    private double CalculateUnsave(IReadOnlyList<GoodModel> goods)
    {
        var validator = new GoodsValidator();
        validator.ValidateAndThrow(goods);

        var volumePrice = CalculatePriceByVolume(goods, out var volume);
        var weightPrice = CalculatePriceByWeight(goods, out var weight);
        var resultPrice = Math.Max(volumePrice, weightPrice);
        _storageRepository.Save(new StorageEntity(volume, resultPrice, DateTime.UtcNow, weight));
        return resultPrice;
    }

    private double CalculatePriceByVolume(IReadOnlyList<GoodModel> goods, out int volume)
    {
        volume = goods.Sum(x => x.Length * x.Height * x.Width);
        return _volumeToPriceRatio * volume / 1000.0d;
    }

    private double CalculatePriceByWeight(IReadOnlyList<GoodModel> goods, out double weight)
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