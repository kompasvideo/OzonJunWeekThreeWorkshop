using Homework.PriceCalculator.Domain.Models;

namespace Homework.PriceCalculator.Domain.Services.Interfaces;

public interface IPriceCalculatorService
{
    double CalculatePrice(IReadOnlyList<GoodModel> goods);
    CalculationLogModel[] QueryLog(int take);
}