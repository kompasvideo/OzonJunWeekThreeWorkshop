using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Separated;
using Homework.PriceCalculator.Domain.Services.Interfaces;

namespace Homework.PriceCalculator.Infrastructure.External;

public class GoodsService : IGoodsService
{
    private readonly List<GoodModel> _goods = new()
    {
        new("Парик для питомца",1,1000,2000,3000,4000,100),
        new("Накидка на телевизор",2,1000,2000,3000,4000,120),
        new("Ковёр настенный",3,2000,3000,4000,5000,140),
        new("Здоровенький ЯЗЬ",4,1000,2000,3000,4000,160),
        new("Билет МММ",5,3000,2000,1000,5000,180),
    };
    
    public IEnumerable<GoodViewModel> GetGoods()
    {
        var rnd = new Random();
        foreach (var model in _goods)
        {
            var count = rnd.Next(0, 10);
            yield return new GoodViewModel(
                model.Name,
                model.Id,
                model.Height,
                model.Length,
                model.Width,
                model.Weight,
                count,
                model.Price
            );
        }
    }

    public record GoodModel(
        string Name,
        int Id, 
        int Height,
        int Length,
        int Width,
        int Weight,
        double Price
        );
}