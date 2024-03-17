using Workshop.Api.Dal.Entities;

namespace Workshop.Api.Dal.Repositories.Interfaces;

public interface IGoodsRepository
{
    void AddOrUpdate(GoodViewModel viewModel);
    ICollection<GoodViewModel> GetAll();
    GoodViewModel Get(int id);
}