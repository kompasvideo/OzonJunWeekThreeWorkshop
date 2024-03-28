using Homework.PriceCalculator.Domain.Entities;

namespace Homework.PriceCalculator.Domain.Seporated;

public interface IGoodsRepository
{
    void AddOrUpdate(GoodViewModel viewModel);
    ICollection<GoodViewModel> GetAll();
    GoodViewModel Get(int id);
}