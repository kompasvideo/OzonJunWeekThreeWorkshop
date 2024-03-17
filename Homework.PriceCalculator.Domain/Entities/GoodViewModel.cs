using System.Collections.Generic;

namespace Homework.PriceCalculator.Domain.Entities;

public class GoodsPageViewModel
{
    public ICollection<GoodViewModel> Goods { get; set; } 
}
public record GoodViewModel(
    string Name,
    int Id,
    int Height,
    int Length,
    int Width,
    int Weight,
    int Count,
    double Price
    );