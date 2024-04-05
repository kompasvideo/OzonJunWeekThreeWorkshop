using Homework.PriceCalculator.Domain.Entities;

namespace Homework.PriceCalculator.Domain.Separated;

public interface IGoodsService
{
    IEnumerable<GoodViewModel> GetGoods();
}