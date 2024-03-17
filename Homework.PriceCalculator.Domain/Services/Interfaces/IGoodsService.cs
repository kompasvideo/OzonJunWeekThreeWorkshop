using System.Collections.Generic;
using Homework.PriceCalculator.Domain.Entities;

namespace Homework.PriceCalculator.Domain.Services.Interfaces;

public interface IGoodsService
{
    IEnumerable<GoodViewModel> GetGoods();
}