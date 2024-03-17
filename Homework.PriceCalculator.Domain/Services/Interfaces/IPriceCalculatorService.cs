using Homework.PriceCalculator.Domain.Models;

namespace Homework.PriceCalculator.Domain.Services.Interfaces;

public interface IPriceCalculatorService
{
    double CalculatePrice(GoodModel[] goods);
    CalculationLogModel[] QueryLog(int take);
}